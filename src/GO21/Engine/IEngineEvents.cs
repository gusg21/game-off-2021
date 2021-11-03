using System;
using Microsoft.Xna.Framework;

namespace Engine
{
    public interface IEngineEvents
    {
        void SceneBegin();
        void SceneEnd();

        void BeforeUpdate();
        void Update();
        void AfterUpdate();

        void BeforeDraw();
        void Draw();
        void AfterDraw();
    }
}
