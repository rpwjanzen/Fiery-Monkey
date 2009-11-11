using System;

namespace FieryMonkey {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            using (IfsGame game = new IfsGame()) {
                game.Run();
            }
        }
    }
}

