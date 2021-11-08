using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AsepritePipeline;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GO21Engine.Components
{
    public class CAseprite : Component
    {
        /// <summary>
        /// The spritesheet that contains the animation frames.
        /// </summary>
        public Texture2D Sheet { get; private set; }
        /// <summary>
        /// The index of the current frame of animation in the current tag.
        /// </summary>
        public AsepriteFrame CurrentFrame;

        // The time that we've spent on the current frame.
        private float _frameTime = 0f;
        // The JSON of the aseprite file.
        private AsepriteJSON _json;

        public CAseprite(AsepriteJSON json)
        {
            _json = json;

            Sheet = Engine.I.LoadTex(Path.GetFileNameWithoutExtension(_json.meta.image));
            
        }

        public AsepriteFrame GetFrame(string name)
        {
            return _json.frames[name];
        }

        /// <summary>
        /// Gets a frame by its index in the list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AsepriteFrame GetFrame(int index)
        {
            return _json.frames.ElementAt(index).Value;
        }

        public override void Update()
        {
            _frameTime += Engine.I.DeltaTime;

            base.Update();
        }

        public override void Draw()
        {
            Engine.I.Drawing.DrawTex(Sheet, Vector2.Zero);

            base.Draw();
        }
    }
}
