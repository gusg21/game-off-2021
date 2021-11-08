using System;
namespace GO21Engine
{
    public class Component
    {
        /// <summary>
        /// The ComponentActor that has this Component in it.
        /// </summary>
        public Actor Parent;
        /// <summary>
        /// Shortcut to the scene with this component.
        /// </summary>
        public Scene Scene => Parent.Scene;

        /// <summary>
        /// Should the Component Draw()?
        /// </summary>
        public bool Visible;
        /// <summary>
        /// Should the Component Update()?
        /// </summary>
        public bool Active;

        /// <summary>
        /// Create a component.
        /// </summary>
        public Component() { }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called once per frame.
        /// </summary>
        public virtual void Draw() { }
    }
}
