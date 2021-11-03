using System;
using Microsoft.Xna.Framework.Graphics;

namespace GO21Engine.Util
{
    public static class Draw
    {
        public static SpriteBatch SpriteBatch { get; private set; }

        static Draw()
        {
            SpriteBatch = new SpriteBatch(Engine.Instance.GraphicsDevice);
        }
    }
}
