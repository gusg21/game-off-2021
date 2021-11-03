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
            GO21Engine.Engine engine = new GO21Engine.Engine(320, 240, 4, "game");
            GO21Engine.Engine.Scene = new PlayScene();
            engine.Run();
        }
    }
}
