using System;
using Microsoft.Xna.Framework;

namespace Engine
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

        /// <summary>
        /// The Z-index of the actor, or its simulated distance from the camera.
        /// </summary>
        public int Depth;
        /// <summary>
        /// The position of the actor.
        /// </summary>
        public Vector2 Position;
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
        public void BeforeUpdate() { }

        /// <summary>
        /// Happens just after <see cref="Update"/>.
        /// </summary>
        public void AfterUpdate() { }

        /// <summary>
        /// Happens just before <see cref="Draw"/>.
        /// </summary>
        public void BeforeDraw() { }

        /// <summary>
        /// Happens just after <see cref="Draw"/>.
        /// </summary>
        public void AfterDraw() { }

        #endregion
    }
}
