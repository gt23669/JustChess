using System;
using JustChess.Board;
using JustChess.Board.Contracts;
using JustChess.Common;
using JustChess.Engine;
using JustChess.Engine.Contracts;
using JustChess.Engine.Initializations;
using JustChess.Figures;
using JustChess.InputProviders.Contracts;
using JustChess.Renderers;
using JustChess.Renderers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject1.InputProviders;
using UnitTestProject1.Renderers;

namespace UnitTestProject1
{
    [TestClass]
    public class NormalChessTest
    {
        private IRenderer renderer;
        private IGameInitializationStrategy gameInitializationStrategy;
        private bool testing;

        [TestInitialize]
        public void Init()
        {
            gameInitializationStrategy = new StandardStartGameInitializationStrategy();
            renderer = new MockConsoleRenderer();
            testing = true;
        }

        [TestMethod]
        public void MovePawn()
        {
            var inputProvider = new MockInputProvider("a2-a3");
            var standardTwoPlayerEngine = new StandardTwoPlayerEngine(renderer, inputProvider, testing);
            standardTwoPlayerEngine.Initialize(gameInitializationStrategy, false);

            PrivateObject chessEngine = new PrivateObject(standardTwoPlayerEngine);
            IBoard board = chessEngine.GetField("board") as Board;
            var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(3, 'a'));
            Assert.IsNull(figure);

            standardTwoPlayerEngine.Start();

            figure = board.GetFigureAtPosition(Position.FromChessCoordinates(3, 'a'));
            Assert.IsNotNull(figure);
            Assert.IsTrue(figure.GetType() == typeof(Pawn));
        }

        [TestMethod]
        public void MoveKnight()
        {
            var inputProvider = new MockInputProvider("b1-c3");
            var standardTwoPlayerEngine = new StandardTwoPlayerEngine(renderer, inputProvider, testing);
            standardTwoPlayerEngine.Initialize(gameInitializationStrategy, false);

            PrivateObject chessEngine = new PrivateObject(standardTwoPlayerEngine);
            IBoard board = chessEngine.GetField("board") as Board;
            var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(3, 'c'));
            Assert.IsNull(figure);

            standardTwoPlayerEngine.Start();

            figure = figure = board.GetFigureAtPosition(Position.FromChessCoordinates(3, 'c'));
            Assert.IsNotNull(figure);
            Assert.IsTrue(figure.GetType() == typeof(Knight));

        }
    }
}
