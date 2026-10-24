using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class BoxComponentDataModel : IComponentDataModel
    {
        public Vector3 Size { get; set; }

        /// <summary>
        /// Cutouts in the order they should be applied. Replaces the old pair of
        /// side-keyed collections, which allowed only one cutout per wall per kind.
        /// </summary>
        public List<Cutout> Cutouts { get; set; }

        public bool RoundBottom { get; set; }

        /// <summary>
        /// Thickness of the surrounding part's wall on each side of this component.
        /// Cutout depth is measured inward from the OUTER face of that wall, so the
        /// placement needs to know how far out it sits. Filled in by InsertBox, which is
        /// the only thing that knows both the part size and where this component landed
        /// in it; left at zero when a component is rendered on its own.
        /// </summary>
        public WallThickness Walls { get; set; } = WallThickness.None;

        public BoxComponentDataModel(Vector3 size)
        {
            Size = size;
            Cutouts = [];
        }

        public IComponent GetComponent()
        {
            return new BoxComponent(this);
        }
    }
}
