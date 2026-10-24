using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class BoxComponentModel(string key, Vector3 pos, IComponentDataModel data)
    {
        public string Key { get; } = key;
        public Vector3 Pos { get; } = pos;
        public IComponentDataModel Data { get; } = data;
    }
}
