using System;
using GO21Engine;
using Game.Scenes;

namespace Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Engine engine = new Engine(320, 240, 3, "game");
            Engine.Scene = new PlayScene();
            engine.Run();
        }
    }
}
