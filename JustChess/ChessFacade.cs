namespace JustChess
{
    using System;

    using Engine;
    using Engine.Initializations;
    using InputProviders;
    using Renderers;

    public static class ChessFacade
    {
        public static void Start()
        {
            Console.WriteLine("Would you like to play Chess960?(y/n)");
            string input = Console.ReadLine();
            bool chess960 = false;
            if (input.ToLower() == "y")
            {
                chess960 = true;
            }
            var renderer = new ConsoleRenderer();
            //// renderer.RenderMainMenu();

            var inputProvider = new ConsoleInputProvider();

            var chessEngine = new StandardTwoPlayerEngine(renderer, inputProvider);

            var gameInitializationStrategy = new StandardStartGameInitializationStrategy();

            chessEngine.Initialize(gameInitializationStrategy, chess960);
            chessEngine.Start();

            Console.ReadLine();
        }
    }
}
