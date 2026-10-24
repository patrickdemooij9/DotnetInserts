namespace DotNetInserts.Core.Models
{
    public class HexagonComponentDataModel
    {
        public double Size { get; }
        public double Height { get; }

        public HexagonComponentDataModel(double size, double height)
        {
            Size = size;
            Height = height;
        }
    }
}
