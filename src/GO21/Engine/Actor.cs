using System;
using GO21Engine.Util;
using Microsoft.Xna.Framework;

namespace GO21Engine
{
    public class Actor
    {
        // == Flags ==

        /// <summary>
        /// Should the actor render?
        /// </summary>
        public bool Visible = true;
        /// <summary>
        /// Should the actor update?
        /// </summary>
        public bool Active = true;
        /// <summary>
        /// Should the enemy be able to collide or be collided with?
        /// </summary>
        public bool Collidable = true;

        // == Attributes ==

        // DO NOT USE; The raw z-index.
        private int _depth;
        /// <summary>
        /// The Z-index of the actor, or its simulated distance from the camera.
        /// </summary>
        public int Depth
        {
            get => _depth;
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
            get => Position.X;
            set => Position.X = value;
        }
        /// <summary>
        /// The Y component of the position of this Actor.
        /// </summary>
        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }
        /// <summary>
        /// The collider for this actor. If null then this actor won't collide.
        /// </summary>
        public Collider Collider { get; private set; }

        // == References ==

        /// <summary>
        /// The ActorList that contains this Actor.
        /// </summary>
        public ActorList List { get; private set; }
        /// <summary>
        /// The Scene that contains the ActorList that contains this Actor.
        /// </summary>
        public Scene Scene => List.Scene;

        /// <summary>
        /// Create a new Actor at the given position.
        /// </summary>
        /// <param name="position"></param>
        public Actor(Vector2 position) => Position = position;

        /// <summary>
        /// Create a new actor.
        /// </summary>
        public Actor() : this(Vector2.Zero) { }

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

        #region Collision

        /// <summary>
        /// Are we overlapping with another actor?
        /// </summary>
        /// <param name="other">The other actor.</param>
        /// <returns>Do we overlap?</returns>
        public bool Collide(Actor other) => Collider.Collide(other.Collider);

        /// <summary>
        /// Are we overlapping with another collider?
        /// </summary>
        /// <param name="other">The other collider.</param>
        /// <returns>Do we overlap?</returns>
        public bool Collide(Collider other) => Collider.Collide(other);

        /// <summary>
        /// If we move to a position, do we collide with another actor?
        /// </summary>
        /// <param name="other">The actor to check against.</param>
        /// <param name="position">The position to check at.</param>
        /// <returns></returns>
        public bool CollideAt(Actor other, Vector2 position)
        {
            Collider collider = new(
                Collider.X + position.X, Collider.Y + position.Y, Collider.W, Collider.H
            );

            return collider.Collide(other.Collider);
        }

        #endregion
    }
}
