using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = AsepritePipeline.AsepriteJSON;
using TOutput = AsepritePipeline.AsepriteJSON;

namespace AsepritePipeline
{
    [ContentProcessor(DisplayName = "* Aseprite Processor")]
    class AsepriteProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            return input;
        }
    }
}
