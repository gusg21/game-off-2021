using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

using TImport = AsepritePipeline.AsepriteJSON;

namespace AsepritePipeline
{
    [ContentImporter(".json", DisplayName = "* Aseprite Importer", DefaultProcessor = "AsepriteProcessor")]
    public class AsepriteImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            Console.WriteLine($"Importing JSON from {filename}...");
            string jsonText = File.ReadAllText(filename);
            AsepriteJSON json = JsonConvert.DeserializeObject<AsepriteJSON>(jsonText);
            return json;
        }
    }
}
