using System;
using GO21Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Scenes
{
    public class PlayScene : GO21Engine.Scene
    {
        Texture2D _penguin;
        Texture2D _windowCorners;

        public override void SceneBegin()
        {
            Engine.ClearColor = Color.BlueViolet;

            _penguin = Engine.LoadTex("penguin");
            _windowCorners = Engine.LoadTex("windowcorners");

            base.SceneBegin();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            Engine.Drawing.Texture(_penguin, 0, 0);
            Engine.Drawing.Texture(_windowCorners, 0, 0);

            base.Draw();
        }
    }
}
