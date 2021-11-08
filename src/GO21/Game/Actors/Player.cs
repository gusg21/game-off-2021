using System;
using AsepritePipeline;
using GO21Engine;
using GO21Engine.Components;
//using MonoGame.Aseprite.Documents;

namespace Game.Actors
{
    public class Player : ComponentActor
    {
        public Player()
        {
            Add(new CAseprite(Engine.I.Content.Load<AsepriteJSON>("penguin_")));
        }
    }
}
