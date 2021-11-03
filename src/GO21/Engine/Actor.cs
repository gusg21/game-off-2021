using System;
namespace Engine
{
    public class Actor : IEngineEvents
    {
        public Actor() { }

        public virtual void SceneBegin() { }

        public virtual void SceneEnd() { }

        public virtual void Update() { }

        public virtual void Draw() { }
    }
}
