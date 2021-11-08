using System;
using AsepritePipeline;
using GO21Engine;
using GO21Engine.Components;
//using MonoGame.Aseprite.Documents;

namespace Game.Actors
{
    public class Player : ComponentActor
    {
        private CAseprite ase;

        public Player()
        {
            ase = Add(new CAseprite(Engine.I.Content.Load<AsepriteJSON>("penguin_")));
            ase.Play("rotate");
        }
    }
}
