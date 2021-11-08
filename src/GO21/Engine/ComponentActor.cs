using System;
using System.Collections.Generic;

namespace GO21Engine
{
    public class ComponentActor : Actor
    {
        /// <summary>
        /// The list of components in this Actor.
        /// </summary>
        public List<Component> Components { get; internal set; }

        public ComponentActor() => Components = new();

        public T Add<T>(T component) where T : Component
        {
            Components.Add(component);
            component.Parent = this;
            return component;
        }

        public T[] Add<T>(params T[] components)
        {
            foreach (var component in components)
                Add(component);

            return components;
        }

        public override void Update()
        {
            foreach (var component in Components)
                if (component.Active)
                    component.Update();

            base.Update();
        }

        public override void Draw()
        {
            foreach (var component in Components)
                if (component.Visible)
                    component.Draw();

            base.Draw();
        }
    }
}
