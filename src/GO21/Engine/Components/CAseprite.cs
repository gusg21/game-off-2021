using System;
using System.Collections.Generic;
using System.IO;
using AsepritePipeline;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GO21Engine.Components
{
    public class CAseprite : Component
    {
        public Texture2D Sheet { get; private set; }

        private AsepriteJSON _json;

        public CAseprite(AsepriteJSON json)
        {
            _json = json;

            Sheet = Engine.I.LoadTex(Path.GetFileNameWithoutExtension(_json.meta.image));
            
        }

        public override void Draw()
        {
            Engine.I.Drawing.DrawTex(Sheet, Vector2.Zero);

            base.Draw();
        }
    }
}
