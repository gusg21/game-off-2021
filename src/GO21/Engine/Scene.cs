using System;
using GO21Engine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GO21Engine
{
    public class Scene
    {
        // == Flags ==

        /// <summary>
        /// Is the scene paused?
        /// </summary>
        public bool Paused = false;
        /// <summary>
        /// Is the scene currently in focus?
        /// </summary>
        public bool Focused { get; private set; }

        // == Time ==

        /// <summary>
        /// The time that this scene has been active, with pausing taken into account.
        /// </summary>
        public float TimeActive { get; private set; }
        /// <summary>
        /// The time that this scene has been active, WITHOUT pausing taken into account.
        /// </summary>
        public float RawTimeActive { get; private set; }

        // == Actors ==

        /// <summary>
        /// The list of actors in the scene.
        /// </summary>
        public ActorList Actors { get; private set; }

        // == Shortcuts ==

        public ContentManager Content
        {
            get
            {
                return Engine.Instance.Content;
            }
        }
        
        public Scene()
        {
            Focused = false;

            Actors = new ActorList(this);
        }

        #region Events

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

        public virtual void BeforeUpdate()
        {
            if (!Paused)
                TimeActive += Engine.Instance.DeltaTime;
            RawTimeActive += Engine.Instance.RawDeltaTime;

            Actors.BeforeUpdate();
        }

        public virtual void AfterUpdate()
        {
            Actors.AfterUpdate();
        }

        public virtual void BeforeDraw()
        {
            Actors.BeforeDraw();
        }

        public virtual void AfterDraw()
        {
            Actors.AfterDraw();
        }

        #endregion

        #region Interval

        /// <summary>
        /// Returns whether the Scene timer has passed the given time interval since the last frame. Ex: given 2.0f, this will return true once every 2 seconds
        /// </summary>
        /// <param name="interval">The time interval to check for</param>
        /// <returns></returns>
        public bool OnInterval(float interval)
        {
            return (int)((TimeActive - Engine.Instance.DeltaTime) / interval) < (int)(TimeActive / interval);
        }

        /// <summary>
        /// Returns whether the Scene timer has passed the given time interval since the last frame. Ex: given 2.0f, this will return true once every 2 seconds
        /// </summary>
        /// <param name="interval">The time interval to check for</param>
        /// <returns></returns>
        public bool OnInterval(float interval, float offset)
        {
            return Math.Floor((TimeActive - offset - Engine.Instance.DeltaTime) / interval) < Math.Floor((TimeActive - offset) / interval);
        }

        public bool BetweenInterval(float interval)
        {
            return Calc.BetweenInterval(TimeActive, interval);
        }

        public bool OnRawInterval(float interval)
        {
            return (int)((RawTimeActive - Engine.Instance.RawDeltaTime) / interval) < (int)(RawTimeActive / interval);
        }

        public bool OnRawInterval(float interval, float offset)
        {
            return Math.Floor((RawTimeActive - offset - Engine.Instance.RawDeltaTime) / interval) < Math.Floor((RawTimeActive - offset) / interval);
        }

        public bool BetweenRawInterval(float interval)
        {
            return Calc.BetweenInterval(RawTimeActive, interval);
        }

        #endregion

        #region Shortcuts

        /// <summary>
        /// Adds an Actor to the scene. Shortcut for Scene.Actors.Add().
        /// </summary>
        /// <param name="actor">The Actor to add to the scene.</param>
        public void Add(Actor actor)
        {
            Actors.Add(actor);
        }

        /// <summary>
        /// Adds multiple Actors to the scene. Shortcut for Scene.Actors.Add().
        /// </summary>
        /// <param name="actors">The Actors to add to the scene.</param>
        public void Add(params Actor[] actors)
        {
            Actors.Add(actors);
        }

        #endregion
    }
}
