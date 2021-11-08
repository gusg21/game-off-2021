using System;
using Microsoft.Xna.Framework;

namespace GO21Engine.Util
{
    public class Collider
    {
        /// <summary>
        /// Dimensions of the collider's rectangle.
        /// </summary>
        public float X, Y, W, H;
        /// <summary>
        /// The position of the rectangle.
        /// </summary>
        public Vector2 Position
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// The size of the rectangle.
        /// </summary>
        public Vector2 Size
        {
            get => new(W, H);
            set
            {
                W = value.X;
                H = value.Y;
            }
        }
        /// <summary>
        /// The center of the rectangle.
        /// </summary>
        public Vector2 Center
        {
            get => new(X + W / 2, Y + H / 2);
            set
            {
                X = value.X - W / 2;
                Y = value.Y - H / 2;
            }
        }
        /// <summary>
        /// The Y of the top of the rectangle.
        /// </summary>
        public float Top => Y;
        /// <summary>
        /// The X of the right of the rectangle.
        /// </summary>
        public float Right => X + W;
        /// <summary>
        /// The Y of the bottom of the rectangle.
        /// </summary>
        public float Bottom => Y + H;
        /// <summary>
        /// The X of the left of the rectangle.
        /// </summary>
        public float Left => X;

        /// <summary>
        /// Create a collider with all zeros.
        /// </summary>
        public Collider()
        {
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        /// <summary>
        /// Create a collider with a given size and no offset.
        /// </summary>
        /// <param name="w">Width of the collider./param>
        /// <param name="h">Height of the collder.</param>
        public Collider(float w, float h)
        {
            X = 0;
            Y = 0;
            W = w;
            H = h;
        }

        /// <summary>
        /// Creates a collider with a size and an offset.
        /// </summary>
        /// <param name="x">The X offset.</param>
        /// <param name="y">The Y offset.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public Collider(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        /// <summary>
        /// Does a collider overlap with another collider?
        /// </summary>
        /// <param name="other">The collider to check.</param>
        /// <returns>Do they overlap?</returns>
        public bool Collide(Collider other) => Left < other.Right && Right > other.Left && Top > other.Bottom && Bottom < other.Top;
    }
}
