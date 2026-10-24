using DotNetInserts.Core.JsonLoader;

namespace DotNetInserts.Projects.Test
{
    public class InsertJsonTest
    {
        public void Render()
        {
            var jsonLoader = new JsonInsertLoader();
            jsonLoader.Load("json1.json").Render(out _);
        }
    }
}
