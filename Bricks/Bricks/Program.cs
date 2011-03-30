using System;

namespace Bricks
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Bricks game = new Bricks())
            {
                game.Run();
            }
        }
    }
#endif
}

