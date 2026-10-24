using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.JsonLoader.Models.Cutouts;
using DotNetInserts.Core.JsonLoader.Models.Cutouts.Legacy;

namespace DotNetInserts.Core.JsonLoader.Mappers.Cutouts
{
    public class CutoutMapper
    {
        /// <summary>
        /// Builds the cutout list for a component from the current `cutouts` array plus,
        /// if present, the legacy side-keyed collections.
        /// </summary>
        public List<Cutout> Map(
            CutoutModel[]? cutouts,
            LegacyCutoutsModel? legacyThrough,
            LegacyCutoutsModel? legacyNotch,
            JsonInsertContext context)
        {
            var result = new List<Cutout>();

            foreach (var cutout in cutouts ?? [])
            {
                result.Add(new Cutout(cutout.Side, cutout.Mode, cutout.ToShape(context)));
            }

            // Order matters: the cutouts are unioned in sequence, so keeping the original
            // traversal (all through-cutouts, then all notches, bottom/top/right/left
            // within each) means a legacy file still generates byte-identical OpenSCAD -
            // and so still hits the same entry in the STL render cache.
            AddLegacy(result, legacyThrough, CutoutMode.Through, context);
            AddLegacy(result, legacyNotch, CutoutMode.Notch, context);

            return result;
        }

        private static void AddLegacy(
            List<Cutout> target, LegacyCutoutsModel? model, CutoutMode mode, JsonInsertContext context)
        {
            if (model is null) return;

            Add(target, model.Bottom, SideType.BOTTOM, mode, context);
            Add(target, model.Top, SideType.TOP, mode, context);
            Add(target, model.Right, SideType.RIGHT, mode, context);
            Add(target, model.Left, SideType.LEFT, mode, context);
        }

        private static void Add(
            List<Cutout> target, ILegacyCutoutModel? model, SideType side, CutoutMode mode, JsonInsertContext context)
        {
            if (model is null) return;

            var offset = model.Position?.ToVector3(context);
            ICutoutShape shape = model switch
            {
                LegacyCubeCutoutModel cube => new CutoutCube
                {
                    Size = cube.Size,
                    Offset = offset,
                    RoundRadius = cube.RoundRadius
                },
                LegacyScoopCutoutModel scoop => new ScoopCutout
                {
                    Size = scoop.Size,
                    Offset = offset,
                    CurveSize = scoop.CurveSize ?? 10
                },
                _ => throw new ArgumentException($"Unsupported legacy cutout type {model.GetType().Name}")
            };

            target.Add(new Cutout(side, mode, shape));
        }
    }
}
