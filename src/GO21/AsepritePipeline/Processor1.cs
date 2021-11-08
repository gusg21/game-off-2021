using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = AsepritePipeline.AsepriteJSON;
using TOutput = System.String;

namespace AsepritePipeline
{
    [ContentProcessor(DisplayName = "Processor1")]
    class Processor1 : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            return default(TOutput);
        }
    }
}
