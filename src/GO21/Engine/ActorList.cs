using System;
using System.Collections.Generic;
using System.Linq;

namespace GO21Engine
{
    public class ActorList
    {
        // The scene that contains this list.
        public Scene Scene { get; private set; }

        // The raw list of actors.
        private List<Actor> _actors;

        // A list of actors queued to be added at the end of the frame.
        private List<Actor> _actorsToAdd;

        // Do we need to sort the actors?
        private bool _unsorted;

        /// <summary>
        /// Creates a manager for all the actors in a scene.
        /// </summary>
        public ActorList(Scene scene)
        {
            _actors = new List<Actor>();
            _actorsToAdd = new List<Actor>();
        }

        #region Events

        /// <summary>
        /// Called when the scene begins or gains focus.
        /// </summary>
        public void SceneBegin()
        {
            foreach (var actor in _actors)
                actor.SceneBegin();
        }

        /// <summary>
        /// Called when the scene ends or loses focus.
        /// </summary>
        public void SceneEnd()
        {
            foreach (var actor in _actors)
                actor.SceneEnd();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void Update()
        {
            foreach (var actor in _actors)
                if (actor.Active)
                    actor.Update();
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void Draw()
        {
            foreach (var actor in _actors)
                if (actor.Visible)
                    actor.Draw();
        }

        /// <summary>
        /// Called just before <see cref="Update"/>.
        /// </summary>
        public void BeforeUpdate()
        {
            foreach (var actor in _actors)
                actor.BeforeUpdate();
        }

        /// <summary>
        /// Called just after <see cref="Update"/>.
        /// </summary>
        public void AfterUpdate()
        {
            foreach (var actor in _actors)
                actor.AfterUpdate();

            UpdateLists();
        }

        /// <summary>
        /// Called just before <see cref="Draw"/>.
        /// </summary>
        public void BeforeDraw()
        {
            foreach (var actor in _actors)
                actor.BeforeDraw();
        }

        /// <summary>
        /// Called just after <see cref="Draw"/>.
        /// </summary>
        public void AfterDraw()
        {
            foreach (var actor in _actors)
                actor.AfterDraw();
        }

        #endregion

        #region Actors and Sorting

        /// <summary>
        /// Update the actors list with the actors that need to be added.
        /// </summary>
        public void UpdateLists()
        {
            foreach (var actor in _actorsToAdd)
            {
                _actors.Add(actor);
            }

            if (_actorsToAdd.Count > 0)
                _actorsToAdd.Clear();

            if (_unsorted)
            {
                    _actors = _actors.OrderBy(actor => actor.Depth).ToList();
                _unsorted = false;
            }
        }

        /// <summary>
        /// Marks the actor list as needing a re-sort. This happens in AfterUpdate().
        /// </summary>
        public void ReSort()
        {
            _unsorted = true;
        }

        /// <summary>
        /// Adds an Actor to the list.
        /// </summary>
        /// <param name="actor">The Actor to add.</param>
        public void Add(Actor actor)
        {
            _actorsToAdd.Add(actor);
            _unsorted = true;
        }

        /// <summary>
        /// Adds Actors to the list.
        /// </summary>
        /// <param name="actors">The Actors to add.</param>
        public void Add(params Actor[] actors)
        {
            foreach (var actor in actors)
                Add(actor);
        }

        #endregion
    }
}
