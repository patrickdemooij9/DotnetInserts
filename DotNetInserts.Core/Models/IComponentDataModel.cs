using DotNetInserts.Core.CustomBlocks;
using OSCADSharp.Spatial;
using System.Text.Json.Serialization;

namespace DotNetInserts.Core.Models
{

    [JsonDerivedType(typeof(BoxComponentDataModel), typeDiscriminator: "box")]
    public interface IComponentDataModel
    {
        Vector3 Size { get; set; }
        IComponent GetComponent();
    }
}
