using JustChess.Board;
using JustChess.Board.Contracts;
using JustChess.Common;
using JustChess.Engine;
using JustChess.Engine.Contracts;
using JustChess.Engine.Initializations;
using JustChess.Figures;
using JustChess.Figures.Contracts;
using JustChess.Renderers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UnitTestProject1.InputProviders;
using UnitTestProject1.Renderers;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class Chess960Test
    {
        private IRenderer renderer;
        private IGameInitializationStrategy gameInitializationStrategy;
        private bool testing;

        private List<Type> normalChessPieceOrder;


        [TestInitialize]
        public void Init()
        {
            gameInitializationStrategy = new StandardStartGameInitializationStrategy();
            renderer = new MockConsoleRenderer();
            testing = true;

            normalChessPieceOrder = new List<Type>
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
        public void ValidChess960SetUp()
        {
            var inputProvider = new MockInputProvider();
            var standardTwoPlayerEngine = new StandardTwoPlayerEngine(renderer, inputProvider, testing);
            standardTwoPlayerEngine.Initialize(gameInitializationStrategy, true);

            PrivateObject chessEngine = new PrivateObject(standardTwoPlayerEngine);
            IBoard board = chessEngine.GetField("board") as Board;

            for (int i = (int)'a'; i <= (int)'h'; i++)
            {
                var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(2, (char)i));
                Assert.IsTrue(figure.GetType() == typeof(Pawn));
            }

            List<Type> boardPieces = new List<Type>();
            for (int i = (int)'a'; i <= (int)'h'; i++)
            {
                var figure = board.GetFigureAtPosition(Position.FromChessCoordinates(1, (char)i));
                boardPieces.Add(figure.GetType());
            }


            var same = true;
            for(int i = 0; i < boardPieces.Count; i++)
            {
                if (boardPieces[i] != normalChessPieceOrder[i])
                {
                    same = false;
                    break;
                }
            }

            Assert.IsFalse(same);
        }
    }
}
