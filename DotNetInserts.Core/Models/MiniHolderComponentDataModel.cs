using System.Numerics;

namespace DotNetInserts.Core.Models
{
    public class MiniHolderComponentDataModel
    {
        public float BaseRadius { get; }
        public float BaseHeight { get; }
        public float Height { get; }
        public float HolderThickness { get; } = 1;

        public MiniHolderComponentDataModel(float baseRadius, float baseHeight, float height)
        {
            BaseRadius = baseRadius;
            BaseHeight = baseHeight;
            Height = height;
        }
    }
}
