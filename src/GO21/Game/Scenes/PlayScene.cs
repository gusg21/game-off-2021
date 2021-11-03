using System;
using GO21Engine;
using Microsoft.Xna.Framework;

namespace Game.Scenes
{
    public class PlayScene : GO21Engine.Scene
    {
        public override void SceneBegin()
        {
            GO21Engine.Engine.ClearColor = Color.BlueViolet;

            base.SceneBegin();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
