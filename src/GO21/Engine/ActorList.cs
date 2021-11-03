using System;
using System.Collections.Generic;

namespace Engine
{
    public class ActorList : IEngineEvents
    {
        private List<Actor> _actorList;

        public ActorList() { }

        public void SceneBegin()
        {
            foreach (var actor in _actorList)
                actor.SceneBegin();
        }

        public void SceneEnd()
        {
            foreach (var actor in _actorList)
                actor.SceneEnd();
        }

        public void Update()
        {
            foreach (var actor in _actorList)
                actor.Update();
        }

        public void Draw()
        {
            foreach (var actor in _actorList)
                actor.Draw();
        }
    }
}
