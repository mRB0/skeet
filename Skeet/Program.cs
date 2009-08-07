using System;

namespace Skeet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SkeetGame game = new SkeetGame())
            {
                game.Run();
            }
        }
    }
}

