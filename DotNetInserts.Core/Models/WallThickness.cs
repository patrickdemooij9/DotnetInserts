using DotNetInserts.Core.Enums;

namespace DotNetInserts.Core.Models
{
    /// <summary>
    /// How much material sits between a component and the outside of the part it lives
    /// in, per side. Used to anchor cutouts to the outer surface.
    /// </summary>
    public sealed record WallThickness(double Top, double Right, double Bottom, double Left)
    {
        public static readonly WallThickness None = new(0, 0, 0, 0);

        public double For(SideType side) => side switch
        {
            SideType.TOP => Top,
            SideType.RIGHT => Right,
            SideType.BOTTOM => Bottom,
            SideType.LEFT => Left,
            _ => 0
        };
    }
}
