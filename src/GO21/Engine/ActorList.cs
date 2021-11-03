using System;
using System.Collections.Generic;

namespace GO21Engine
{
    public class ActorList
    {
        // The scene that contains this list.
        public Scene Scene { get; private set; }

        // The raw list of actors.
        private List<Actor> _actorList;

        /// <summary>
        /// Creates a manager for all the actors in a scene.
        /// </summary>
        public ActorList(Scene scene)
        {
            _actorList = new List<Actor>();
        }

        #region Events

        /// <summary>
        /// Called when the scene begins or gains focus.
        /// </summary>
        public void SceneBegin()
        {
            foreach (var actor in _actorList)
                actor.SceneBegin();
        }

        /// <summary>
        /// Called when the scene ends or loses focus.
        /// </summary>
        public void SceneEnd()
        {
            foreach (var actor in _actorList)
                actor.SceneEnd();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void Update()
        {
            foreach (var actor in _actorList)
                actor.Update();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void Draw()
        {
            foreach (var actor in _actorList)
                actor.Draw();
        }

        /// <summary>
        /// Called just before <see cref="Update"/>.
        /// </summary>
        public void BeforeUpdate()
        {
            foreach (var actor in _actorList)
                actor.BeforeUpdate();
        }

        /// <summary>
        /// Called just after <see cref="Update"/>.
        /// </summary>
        public void AfterUpdate()
        {
            foreach (var actor in _actorList)
                actor.AfterUpdate();
        }

        /// <summary>
        /// Called just before <see cref="Draw"/>.
        /// </summary>
        public void BeforeDraw()
        {
            foreach (var actor in _actorList)
                actor.BeforeDraw();
        }

        /// <summary>
        /// Called just after <see cref="Draw"/>.
        /// </summary>
        public void AfterDraw()
        {
            foreach (var actor in _actorList)
                actor.AfterDraw();
        }

        #endregion

        public void UpdateLists()
        {

        }
    }
}
