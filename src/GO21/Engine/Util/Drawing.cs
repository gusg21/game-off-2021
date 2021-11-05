using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GO21Engine.Util
{
    public class Drawing
    {
        /// <summary>
        /// The SpriteBatch to draw to.
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }
        /// <summary>
        /// The Graphics Device for the Game.
        /// </summary>
        /// <param name="device"></param>
        public GraphicsDevice Device { get; private set; }
        /// <summary>
        /// The internal, low-res screen to render to. This is then rendered to
        /// the window itself for perfect pixel art beauty.
        /// </summary>
        public RenderTarget2D InternalScreen { get; private set; }
        /// <summary>
        /// The clear color for the internal screen.
        /// </summary>
        public Color ClearColor;

        public Drawing(GraphicsDevice device, int screenWidth, int screenHeight)
        {
            SpriteBatch = null; /// Filled in <see cref="Initialize"/>
            Device = device;
            InternalScreen = new RenderTarget2D(Device, screenWidth, screenHeight);
        }

        /// <summary>
        /// Called after the Graphics have been set up; creates the SpriteBatch.
        /// </summary>
        public void Initialize()
        {
            SpriteBatch = new SpriteBatch(Device);
        }

        /// <summary>
        /// Begin drawing to internal screen. Sets the sampler state to Point-clamp.
        /// </summary>
        public void Begin()
        {
            Device.SetRenderTarget(InternalScreen);
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }

        /// <summary>
        /// Begin drawing. Sets the sampler state to Point-clamp.
        /// </summary>
        /// <param name="transform">The transform matrix (usually from a camera) to use.</param>
        public void Begin(Matrix transform)
        {
            Device.SetRenderTarget(InternalScreen);
            Device.Clear(ClearColor);
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transform);
        }

        /// <summary>
        /// Flush textures and sprites to the screen.
        /// </summary>
        public void End()
        {
            SpriteBatch.End();
            Device.SetRenderTarget(null); // Point back to window

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            {
                // Draw the internal screen centered in the window.
                SpriteBatch.Draw(InternalScreen, new Rectangle(
                    Engine.WindowWidth / 2 - Engine.ScreenWidth / 2,
                    Engine.WindowHeight / 2 - Engine.ScreenHeight / 2,
                    Engine.ScreenWidth,
                    Engine.ScreenHeight
                ), Color.White);
            }
            SpriteBatch.End();
        }

        #region Texture

        /// <summary>
        /// Draw a texture.
        /// </summary>
        /// <param name="tex">The texture to draw.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        public void Texture(Texture2D tex, float x, float y)
        {
            SpriteBatch.Draw(tex, new Vector2(x, y), Color.White);
        }

        /// <summary>
        /// Draw a texture.
        /// </summary>
        /// <param name="tex">The texture to draw.</param>
        /// <param name="position">The position to draw it at.</param>
        public void Texture(Texture2D tex, Vector2 position)
        {
            SpriteBatch.Draw(tex, position, Color.White);
        }

        /// <summary>
        /// Draw a texture.
        /// </summary>
        /// <param name="tex">The texture to draw.</param>
        /// <param name="position">The position to draw it at.</param>
        /// <param name="color">The color to apply to the texture.</param>
        public void Texture(Texture2D tex, Vector2 position, Color color)
        {
            SpriteBatch.Draw(tex, position, color);
        }

        #endregion
    }
}
