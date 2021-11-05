using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = System.String;
using TOutput = LuaPipeline.LuaScript;

namespace LuaPipeline
{
    [ContentProcessor(DisplayName = "* Lua Processor")]
    class LuaProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            TOutput script = new TOutput();
            script.script = input;
            return script;
        }
    }
}
