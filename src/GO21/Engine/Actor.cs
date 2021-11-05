using System;
using Microsoft.Xna.Framework;

namespace GO21Engine
{
    public class Actor
    {
        // == Flags ==

        /// <summary>
        /// Should the actor render?
        /// </summary>
        public bool Visible;
        /// <summary>
        /// Should the actor update?
        /// </summary>
        public bool Active;
        /// <summary>
        /// Should the enemy be able to collide or be collided with?
        /// </summary>
        public bool Collidable;

        // == Attributes ==

        // DO NOT USE; The raw z-index.
        private int _depth;

        /// <summary>
        /// The Z-index of the actor, or its simulated distance from the camera.
        /// </summary>
        public int Depth
        {
            get
            {
                return _depth;
            }
            set
            {
                // Require re-sort whenever the depth changes
                _depth = value;
                List.ReSort();
            }
        }
        /// <summary>
        /// The position of the actor.
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// The X component of the position of this Actor.
        /// </summary>
        public float X
        {
            get
            {
                return Position.X;
            }
            set
            {
                Position.X = value;
            }
        }
        /// <summary>
        /// The Y component of the position of this Actor.
        /// </summary>
        public float Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                Position.Y = value;
            }
        }

        // == References ==

        /// <summary>
        /// The ActorList that contains this Actor.
        /// </summary>
        public ActorList List { get; private set; }
        /// <summary>
        /// The Scene that contains the ActorList that contains this Actor.
        /// </summary>
        public Scene Scene
        {
            get
            {
                return List.Scene;
            }
        }


        /// <summary>
        /// Create a new actor.
        /// </summary>
        public Actor() { }

        #region Events

        /// <summary>
        /// Called when the Scene that contains this actor is focused/begun.
        /// </summary>
        public virtual void SceneBegin() { }

        /// <summary>
        /// Called when the Scene that contains this actor is defocused/ended.
        /// </summary>
        public virtual void SceneEnd() { }

        /// <summary>
        /// Called every frame. 
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public virtual void Draw() { }

        /// <summary>
        /// Happens just before <see cref="Update"/>.
        /// </summary>
        public virtual void BeforeUpdate() { }

        /// <summary>
        /// Happens just after <see cref="Update"/>.
        /// </summary>
        public virtual void AfterUpdate() { }

        /// <summary>
        /// Happens just before <see cref="Draw"/>.
        /// </summary>
        public virtual void BeforeDraw() { }

        /// <summary>
        /// Happens just after <see cref="Draw"/>.
        /// </summary>
        public virtual void AfterDraw() { }

        /// <summary>
        /// Called when this Actor is added to a list.
        /// </summary>
        /// <param name="list">The ActorList this Actor was added to.</param>
        public virtual void OnAdded(ActorList list)
        {
            List = list;
        }

        #endregion
    }
}
