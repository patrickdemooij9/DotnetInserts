using OSCADSharp;
using System.Collections.Generic;

namespace DotNetInserts.Core.CustomBlocks
{
    public class InsertCollection
    {
        public List<OSCADObject> Items { get; }

        public InsertCollection()
        {
            Items = new List<OSCADObject>();
        }

        public void Render()
        {
            OSCADObject global = null;
            var i = 0;
            foreach (var item in Items)
            {
                if (global is null)
                    global = item;
                else
                {
                    global = global.Union(item);
                }
                item.ToFile($"{i}.scad");
            }
            global.ToFile("all.scad");
        }
    }
}
