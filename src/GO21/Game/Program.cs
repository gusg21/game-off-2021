using System;

namespace Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Engine.Engine engine = new Engine.Engine(320, 240, 4, "game");
        }
    }
}
