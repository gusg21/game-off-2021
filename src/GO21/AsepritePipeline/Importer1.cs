using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

using TImport = AsepritePipeline.AsepriteJSON;

namespace AsepritePipeline
{
    [ContentImporter(".json", DisplayName = "Importer1", DefaultProcessor = "Processor1")]
    public class Importer1 : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            string jsonText = File.ReadAllText(filename);
            TImport json = JsonConvert.DeserializeObject<TImport>(jsonText);
            return json;
        }
    }
}
