using DotNetInserts.Core;
using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using DotNetInserts.Core.JsonLoader.Models.Insert;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using DotNetInserts.Core.JsonLoader.Models.Components;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Tier 0: the dimension system in isolation. These are the cheap tests that tell you
    /// *why* a fixture broke when a snapshot moves.
    /// </summary>
    public class DimensionTests
    {
        private static BoxPartModel Box(string name, double x, double y, double z,
                                        IComponentModel[]? components = null) => new()
        {
            Id = name,
            Name = name,
            Size = Fixed(x, y, z),
            Components = components ?? []
        };

        private static PartDimension Fixed(double x, double y, double z) => new()
        {
            X = new FixedDimension { Size = x },
            Y = new FixedDimension { Size = y },
            Z = new FixedDimension { Size = z }
        };

        private static JsonInsertContext Context(params IPart[] parts)
        {
            var insert = new JsonInsert { Name = "T", Size = new OSCADSharp.Spatial.Vector3(200, 200, 50), Parts = parts };
            return new JsonInsertContext(insert, parts[0]);
        }

        [Fact]
        public void Fixed_returns_its_size()
        {
            var context = Context(Box("A", 10, 20, 30));

            Assert.Equal(12.5, new FixedDimension { Size = 12.5 }.GetSize(context));
        }

        [Fact]
        public void Constant_resolves_to_the_shared_card_constants()
        {
            var context = Context(Box("A", 10, 20, 30));

            Assert.Equal(Constants.CardSizeSleevedX,
                new ConstantDimension { ConstantType = ConstantDimensionType.CardSizeSleevedX }.GetSize(context));
            Assert.Equal(Constants.CardSizeSleevedZ,
                new ConstantDimension { ConstantType = ConstantDimensionType.CardSizeSleevedZ }.GetSize(context));
        }

        [Theory]
        // The decimal separator inside expressions is a comma, matching every JSON file
        // in the repo. Pinned in ComputedDimension so it does not follow machine locale.
        [InlineData("68+1,2", 69.2)]
        [InlineData("55*0,65", 35.75)]
        [InlineData("(10+2)*3", 36)]
        public void Computed_evaluates_literal_expressions(string expression, double expected)
        {
            var context = Context(Box("A", 10, 20, 30));

            Assert.Equal(expected, new ComputedDimension { Expression = expression }.GetSize(context), 6);
        }

        /// <summary>
        /// GOTCHA worth knowing before you write an expression: Flee applies C# numeric
        /// rules, so a division of two integer literals is integer division. "100/8" is
        /// 12, not 12.5 - and the truncation is silent. Write one side as a decimal
        /// ("100/8,0") whenever the result can be fractional. This bites on things like
        /// "#Tray.X/3" for tiling.
        /// </summary>
        [Fact]
        public void Computed_truncates_integer_division()
        {
            var context = Context(Box("A", 10, 20, 30));

            Assert.Equal(12, new ComputedDimension { Expression = "100/8" }.GetSize(context), 6);
            Assert.Equal(12.5, new ComputedDimension { Expression = "100/8,0" }.GetSize(context), 6);
        }

        [Fact]
        public void Computed_resolves_part_references()
        {
            var context = Context(Box("Cards", 70, 100, 30), Box("Tokens", 50, 40, 20));

            Assert.Equal(120, new ComputedDimension { Expression = "#Cards.X + #Tokens.X" }.GetSize(context), 6);
            Assert.Equal(98, new ComputedDimension { Expression = "#Cards.Y - 2" }.GetSize(context), 6);
        }

        [Fact]
        public void Computed_resolves_component_references()
        {
            var component = new BoxComponentModel
            {
                Id = "w",
                Name = "Well",
                Size = Fixed(64, 94, 26)
            };
            var context = Context(Box("Cards", 70, 100, 30, [component]));

            Assert.Equal(26, new ComputedDimension { Expression = "#Cards.Well.Z" }.GetSize(context), 6);
        }

        [Fact]
        public void Computed_reports_an_unknown_part_by_name()
        {
            var context = Context(Box("Cards", 70, 100, 30));

            var ex = Assert.ThrowsAny<Exception>(() =>
                new ComputedDimension { Expression = "#Missing.X" }.GetSize(context));

            Assert.Contains("Missing", ex.Message);
        }

        [Fact]
        public void Computed_reports_an_unknown_property_by_name()
        {
            var context = Context(Box("Cards", 70, 100, 30));

            var ex = Assert.ThrowsAny<Exception>(() =>
                new ComputedDimension { Expression = "#Cards.Depth" }.GetSize(context));

            Assert.Contains("Depth", ex.Message);
        }

        [Fact]
        public void ParentAlign_centres_the_component_against_its_parent()
        {
            var component = new BoxComponentModel { Id = "w", Name = "Well", Size = Fixed(40, 30, 20) };
            var part = Box("Tray", 100, 80, 30, [component]);
            var context = Context(part);
            context.CurrentComponent = component;

            // (80 - 30) / 2 - gap
            Assert.Equal(25 - 2, new ParentAlignDimension { AlignType = AlignDimensionType.Top, Gap = 2 }.GetSize(context));
            Assert.Equal(-(25 - 2), new ParentAlignDimension { AlignType = AlignDimensionType.Bottom, Gap = 2 }.GetSize(context));
            // (100 - 40) / 2 - gap
            Assert.Equal(30 - 5, new ParentAlignDimension { AlignType = AlignDimensionType.Right, Gap = 5 }.GetSize(context));
            Assert.Equal(-(30 - 5), new ParentAlignDimension { AlignType = AlignDimensionType.Left, Gap = 5 }.GetSize(context));
        }

        [Fact]
        public void ParentAlign_without_a_component_is_a_clear_error()
        {
            var context = Context(Box("Tray", 100, 80, 30));

            var ex = Assert.Throws<InvalidOperationException>(() =>
                new ParentAlignDimension { AlignType = AlignDimensionType.Top }.GetSize(context));

            Assert.Contains("CurrentComponent", ex.Message);
        }

        [Fact]
        public void Computed_resolves_braced_references_to_names_with_spaces()
        {
            var component = new BoxComponentModel { Id = "w", Name = "Card well", Size = Fixed(64, 94, 26) };
            var context = Context(Box("Card tray", 70, 100, 30, [component]));

            Assert.Equal(28, new ComputedDimension { Expression = "#{Card tray.Card well.Z} + 2" }.GetSize(context), 6);
            Assert.Equal(130, new ComputedDimension { Expression = "#{Card tray.Y} + 30" }.GetSize(context), 6);
        }

        private static BoxComponentModel Well(string id, double x, double y, PartDimension? location = null) => new()
        {
            Id = id,
            Name = id,
            Size = Fixed(x, y, 20),
            Location = location ?? PartDimension.ZeroDimension()
        };

        private static PartDimension NextTo(string id, AlignDimensionType align, float gap = 0) => new()
        {
            X = new ComponentAlignDimension { ComponentId = id, AlignType = align, Gap = gap },
            Y = new ComponentAlignDimension { ComponentId = id, AlignType = align, Gap = gap },
            Z = new ComponentAlignDimension { ComponentId = id, AlignType = align, Gap = gap }
        };

        private static OSCADSharp.Spatial.Vector3 LocationOf(JsonInsertContext context, IComponentModel component) =>
            context.WithComponent(component, () => component.Location.ToVector3(context));

        [Fact]
        public void ComponentAlign_butts_against_the_target_and_centres_on_the_other_axis()
        {
            var target = Well("a", 40, 30, Fixed(-10, 5, 0));
            var right = Well("b", 20, 10, NextTo("a", AlignDimensionType.Right, 2));
            var above = Well("c", 20, 10, NextTo("a", AlignDimensionType.Top, 2));
            var context = Context(Box("Tray", 150, 150, 30, [target, right, above]));

            // -10 + 40/2 + 2 + 20/2, and Y follows the target's centre.
            Assert.Equal(new OSCADSharp.Spatial.Vector3(22, 5, 0), LocationOf(context, right));
            // 5 + 30/2 + 2 + 10/2, and X follows the target's centre.
            Assert.Equal(new OSCADSharp.Spatial.Vector3(-10, 27, 0), LocationOf(context, above));
        }

        [Fact]
        public void ComponentAlign_chains_through_a_parent_aligned_target()
        {
            var first = Well("a", 40, 30, new PartDimension
            {
                X = new ParentAlignDimension { AlignType = AlignDimensionType.Left, Gap = 2 },
                Y = new FixedDimension { Size = 0 },
                Z = new FixedDimension { Size = 0 }
            });
            var second = Well("b", 40, 30, NextTo("a", AlignDimensionType.Right, 1));
            var third = Well("c", 40, 30, NextTo("b", AlignDimensionType.Right, 1));
            var context = Context(Box("Tray", 150, 80, 30, [first, second, third]));

            // first: -(150 - 40)/2 + 2 = -53; each next one is 40 + 1 further right.
            Assert.Equal(-53 + 41 + 41, LocationOf(context, third).X, 6);
            Assert.Equal(-53 - 41, LocationOf(context, new BoxComponentModel
            {
                Id = "d", Name = "d", Size = Fixed(40, 30, 20), Location = NextTo("a", AlignDimensionType.Left, 1)
            }).X, 6);
        }

        [Fact]
        public void ComponentAlign_reports_a_cycle()
        {
            var a = Well("a", 10, 10, NextTo("b", AlignDimensionType.Right));
            var b = Well("b", 10, 10, NextTo("a", AlignDimensionType.Right));
            var context = Context(Box("Tray", 100, 100, 30, [a, b]));

            var ex = Assert.Throws<InvalidOperationException>(() => LocationOf(context, a));
            Assert.Contains("refers back", ex.Message);
        }

        [Fact]
        public void ComponentAlign_reports_an_unknown_component()
        {
            var a = Well("a", 10, 10, NextTo("missing", AlignDimensionType.Right));
            var context = Context(Box("Tray", 100, 100, 30, [a]));

            var ex = Assert.Throws<ArgumentException>(() => LocationOf(context, a));
            Assert.Contains("missing", ex.Message);
        }

        private static BoxPartModel Placed(string id, double x, double y, PartDimension location)
        {
            var part = Box(id, x, y, 30);
            part.Location = location;
            return part;
        }

        private static PartDimension NextToPart(string id, AlignDimensionType align, float gap = 0) => new()
        {
            X = new PartAlignDimension { PartId = id, AlignType = align, Gap = gap },
            Y = new PartAlignDimension { PartId = id, AlignType = align, Gap = gap },
            Z = new PartAlignDimension { PartId = id, AlignType = align, Gap = gap }
        };

        private static OSCADSharp.Spatial.Vector3 LocationOf(JsonInsertContext context, IPart part) =>
            context.WithPart(part, () => part.Location.ToVector3(context));

        [Fact]
        public void PartAlign_butts_against_the_target_by_its_corner()
        {
            var target = Placed("A", 50, 40, Fixed(10, 20, 0));
            var right = Placed("B", 30, 30, NextToPart("A", AlignDimensionType.Right, 1));
            var left = Placed("C", 30, 30, NextToPart("A", AlignDimensionType.Left, 1));
            var above = Placed("D", 30, 25, NextToPart("A", AlignDimensionType.Top));
            var below = Placed("E", 30, 25, NextToPart("A", AlignDimensionType.Bottom));
            var context = Context(target, right, left, above, below);

            // Parts sit on their minimum corner, so Right/Top add the target's size and
            // Left/Bottom subtract the part's own. The other axis lines up the lower edges.
            Assert.Equal(new OSCADSharp.Spatial.Vector3(61, 20, 0), LocationOf(context, right));
            Assert.Equal(new OSCADSharp.Spatial.Vector3(-21, 20, 0), LocationOf(context, left));
            Assert.Equal(new OSCADSharp.Spatial.Vector3(10, 60, 0), LocationOf(context, above));
            Assert.Equal(new OSCADSharp.Spatial.Vector3(10, -5, 0), LocationOf(context, below));
        }

        [Fact]
        public void PartAlign_chains_through_parts_listed_later()
        {
            // "A" is placed against "B", which comes after it in the file.
            var a = Placed("A", 40, 40, NextToPart("B", AlignDimensionType.Right));
            var b = Placed("B", 50, 40, NextToPart("C", AlignDimensionType.Right));
            var c = Placed("C", 60, 40, Fixed(0, 0, 0));
            var context = Context(a, b, c);

            Assert.Equal(110, LocationOf(context, a).X, 6);
        }

        [Fact]
        public void PartAlign_reports_a_cycle()
        {
            var a = Placed("A", 10, 10, NextToPart("B", AlignDimensionType.Right));
            var b = Placed("B", 10, 10, NextToPart("A", AlignDimensionType.Right));
            var context = Context(a, b);

            var ex = Assert.Throws<InvalidOperationException>(() => LocationOf(context, a));
            Assert.Contains("refers back", ex.Message);
        }
    }
}
