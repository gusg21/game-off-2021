using System;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Scene : IEngineEvents
    {
        public bool Focused { get; private set; }
        public GameTime GameTime { get; private set; }

        public ActorList Actors { get; private set; }
        
        public Scene()
        {
            Focused = false;

            Actors = new ActorList();
        }

        public virtual void SceneBegin()
        {
            Focused = true;

            Actors.SceneBegin();
        }

        public virtual void SceneEnd()
        {
            Focused = false;

            Actors.SceneEnd();
        }

        public virtual void Update()
        {
            Actors.Update();
        }

        public virtual void Draw()
        {
            Actors.Draw();
        }

        public void BeforeUpdate()
        {
            
        }

        public void AfterUpdate()
        {

        }

        public void BeforeDraw()
        {

        }

        public void AfterDraw()
        {

        }
    }
}
