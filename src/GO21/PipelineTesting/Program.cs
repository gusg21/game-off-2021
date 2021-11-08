using System;
using AsepritePipeline;

namespace PipelineTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            AsepriteImporter importer = new AsepriteImporter();
            AsepriteJSON json = importer.Import("test.json", null);
            Console.WriteLine(json.meta.app);
        }
    }
}
