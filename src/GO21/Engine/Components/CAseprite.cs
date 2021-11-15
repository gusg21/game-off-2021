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
        /// Is an animation currently playing?
        /// </summary>
        public bool Playing { get; private set; } = false;
        /// <summary>
        /// The current Aseprite frame.
        /// </summary>
        public AsepriteFrame Frame
        {
            get
            {
                if (_tag != null)
                    return GetFrame(_tag.from + FrameIndex);
                else
                    return GetFrame(0);
            }
        }
        /// <summary>
        /// The index within the current tag that we're drawing.
        /// </summary>
        public int FrameIndex = 0;
        /// <summary>
        /// The multiplier to apply to the speed. 1 = speed defined in the JSON.
        /// </summary>
        public float SpeedScale = 1f;

        // The source JSON for the Aseprite source document.
        private AsepriteJSON _json;
        // The current frame tag we're animating.
        private AsepriteTag _tag;
        // The time we've spent on the current frame.
        private float _frameTime;

        /// <summary>
        /// Create an animation based on the JSON exported by Aseprite.
        /// </summary>
        /// <param name="json"></param>
        public CAseprite(AsepriteJSON json)
        {
            _json = json;
            Sheet = Engine.I.LoadTex(Path.GetFileNameWithoutExtension(_json.meta.image));
        }

        /// <summary>
        /// Play a tag as an animation.
        /// </summary>
        /// <param name="tagName"></param>
        public void Play(string tagName)
        {
            _tag = GetTag(tagName);
            _frameTime = 0f;
            FrameIndex = 0;
            Playing = true;
        }

        /// <summary>
        /// Pause the playing animation but don't reset the progress through it.
        /// </summary>
        public void Pause() => Playing = false;

        /// <summary>
        /// Stop the currently playing animation.
        /// </summary>
        public void Stop()
        {
            _tag = null;
            _frameTime = 0f;
            FrameIndex = 0;
            Playing = false;
        }

        /// <summary>
        /// Find a tag by name. Will return null if the tag was not able to be found.
        /// </summary>
        /// <param name="name">The name of the tag to search for.</param>
        /// <returns>The tag, or null if it was not found.</returns>
        public AsepriteTag GetTag(string name)
        {
            foreach (var tag in _json.meta.frameTags)
                if (tag.name == name)
                    return tag;

            return null;
        }

        /// <summary>
        /// Get the animation frame at a given index.
        /// </summary>
        /// <param name="index">The index to get.</param>
        /// <returns>The frame of animation.</returns>
        public AsepriteFrame GetFrame(int index) => _json.frames.ElementAt(index).Value;

        /// <summary>
        /// Get the frame of animation based off the name of the animation. Check the JSON for reference.
        /// </summary>
        /// <param name="name">The name of the frame.</param>
        /// <returns>The frame of animation to get.</returns>
        public AsepriteFrame GetFrame(string name) => _json.frames[name];

        public override void Update()
        {
            if (Playing)
            {
                _frameTime += Engine.I.DeltaTime * SpeedScale;

                if (_frameTime > Frame.duration / 1000f)
                {
                    FrameIndex++;
                    _frameTime = 0;
                }

                if (FrameIndex > _tag.to - _tag.from)
                    FrameIndex = 0;
            }

            base.Update();
        }

        public override void Draw()
        {
            Engine.I.Drawing.DrawSubTex(Sheet, Vector2.Zero, Frame.frame.ToXnaRect());

            base.Draw();
        }
    }
}
