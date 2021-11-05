using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

using TImport = System.String;

namespace LuaPipeline
{
    [ContentImporter(".txt", DisplayName = "* Lua Importer", DefaultProcessor = "LuaProcessor")]
    public class LuaImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            return File.ReadAllText(filename);
        }
    }
}
