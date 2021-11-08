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

        public ComponentActor()
        {
            Components = new List<Component>();
        }

        public void Add(Component component)
        {
            Components.Add(component);
            component.Parent = this;
        }

        public void Add(params Component[] components)
        {
            foreach (var component in components)
                Add(component);
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
