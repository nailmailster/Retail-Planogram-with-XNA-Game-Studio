using System;

namespace Categories
{
    static class Program
    {
[STAThread]
        public static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}
