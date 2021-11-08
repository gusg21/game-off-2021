using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GO21Engine.Components
{
    public class CAseprite : Component
    {
        // The aseprite library's sprite representation
        private Texture2D sprite;

        /// <summary>
        /// The color to tint this sprite with.
        /// </summary>
        //public Color Color
        //{
        //    get
        //    {
        //        return sprite.Color;
        //    }
        //    set
        //    {
        //        sprite.Color = value;
        //    }
        //}
        ///// <summary>
        ///// Is the animation currently paused?
        ///// </summary>
        //public bool Paused
        //{
        //    get
        //    {
        //        return sprite.Paused;
        //    }
        //}
        //public Dictionary<string, Animation> Animations
        //{
        //    get
        //    {
        //        return sprite.Animations;
        //    }
        //}

        //public CAseprite(AsepriteDocument doc)
        //{
        //    sprite = new AnimatedSprite(doc);
        //}

        #region Game Loop

        public override void Update()
        {
            //sprite.Position = Parent.Position;
            //sprite.Update(Engine.I.DeltaTime);

            base.Update();
        }

        public override void Draw()
        {
            //sprite.Render(Engine.I.Drawing.SpriteBatch);

            base.Draw();
        }

        #endregion

        /// <summary>
        /// Play a given animation by its name.
        /// </summary>
        /// <param name="animationName">The animation to play.</param>
        public void Play(string animationName)
        {
            //sprite.Play(animationName);
        }

        /// <summary>
        /// Pause the playing animation.
        /// </summary>
        public void Pause()
        {
            //sprite.Pause();
        }
    }
}
