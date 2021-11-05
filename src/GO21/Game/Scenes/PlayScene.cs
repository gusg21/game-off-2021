using System;
using GO21Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Scenes
{
    public class PlayScene : Scene
    {
        //Texture2D _penguin;
        Texture2D _windowCorners;

        public override void SceneBegin()
        {
            //_penguin = Engine.LoadTex("penguin");
            _windowCorners = Engine.I.LoadTex("windowcorners");

            Add(new LuaActor(Engine.I.LoadLua("scripts/test")));

            base.SceneBegin();
        }

        public override void Update()
        {
            Engine.I.Camera.Approach(new Vector2(0, 0), 0.01f);

            base.Update();
        }

        public override void Draw()
        {
            //Engine.Drawing.Texture(_penguin, 0, 0);
            Engine.I.Drawing.Texture(_windowCorners, 0, 0);

            base.Draw();
        }
    }
}
