using System;
using System.Collections.Generic;
using JustChess.Board;
using JustChess.Board.Contracts;
using JustChess.Common;
using JustChess.Engine;
using JustChess.Engine.Contracts;
using JustChess.Engine.Initializations;
using JustChess.Figures;
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

        private List<Type> chessPiecesCorrectOrder;

        [TestInitialize]
        public void Init()
        {
            gameInitializationStrategy = new StandardStartGameInitializationStrategy();
            renderer = new MockConsoleRenderer();
            testing = true;

            chessPiecesCorrectOrder = new List<Type>
            {
                typeof(Rook),
                typeof(Knight),
                typeof(Bishop),
                typeof(Queen),
                typeof(King),
                typeof(Bishop),
                typeof(Knight),
                typeof(Rook)
            };
        }

        [TestMethod]
        public void ValidMovePawn()
        {
            var inputProvider = new MockInputProviderWithMove("a2-a3");
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
        public void ValidMoveKnight()
        {
            var inputProvider = new MockInputProviderWithMove("b1-c3");
            var standardTwoPlayerEngine = new StandardTwoPlayerEngine(renderer, inputProvider, testing);
            standardTwoPlayerEngine.Initialize(gameInitializationStrategy, false);

            PrivateObject chessEngine = new PrivateObject(standardTwoPlayerEngine);
            IBoard board = chessEngine.GetField("board") as Board;
            var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(3, 'c'));
            Assert.IsNull(figure);

            standardTwoPlayerEngine.Start();

            figure = board.GetFigureAtPosition(Position.FromChessCoordinates(3, 'c'));
            Assert.IsNotNull(figure);
            Assert.IsTrue(figure.GetType() == typeof(Knight));

        }

        [TestMethod]
        public void InvalidPawnMove()
        {
            var inputProvider = new MockInputProviderWithMove("a2-a5");
            var standardTwoPlayerEngine = new StandardTwoPlayerEngine(renderer, inputProvider, testing);
            standardTwoPlayerEngine.Initialize(gameInitializationStrategy, false);

            PrivateObject chessEngine = new PrivateObject(standardTwoPlayerEngine);
            IBoard board = chessEngine.GetField("board") as Board;
            standardTwoPlayerEngine.Start();

            var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(5, 'a'));
            Assert.IsNull(figure);

            var pawn = board.GetFigureAtPosition(Position.FromChessCoordinates(2, 'a'));
            Assert.IsNotNull(pawn);
            Assert.IsTrue(pawn.GetType() == typeof(Pawn));
        }


        [TestMethod]
        public void ValidChessSetUp()
        {
            var inputProvider = new MockInputProvider();
            var standardTwoPlayerEngine = new StandardTwoPlayerEngine(renderer, inputProvider, testing);
            standardTwoPlayerEngine.Initialize(gameInitializationStrategy, false);

            PrivateObject chessEngine = new PrivateObject(standardTwoPlayerEngine);
            IBoard board = chessEngine.GetField("board") as Board;

            for (int i = (int)'a'; i <= (int)'h'; i++)
            {
                var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(2, (char)i));
                Assert.IsTrue(figure.GetType() == typeof(Pawn));
            }

            int index = 0;
            for (int i = (int)'a'; i <= (int)'h'; i++)
            {
                var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(1, (char)i));
                Assert.IsTrue(figure.GetType() == chessPiecesCorrectOrder[index++]);
            }
        }
    }
}
