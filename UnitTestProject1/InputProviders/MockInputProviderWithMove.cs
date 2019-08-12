using JustChess.Common;
using JustChess.Common.Console;
using JustChess.InputProviders.Contracts;
using JustChess.Players;
using JustChess.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.InputProviders
{
    public class MockInputProviderWithMove : IInputProvider
    {
        public string Move { get; set; }

        public MockInputProviderWithMove(string move)
        {
            this.Move = move;
        }
        public Move GetNextPlayerMove(IPlayer player)
        {
            return ConsoleHelpers.CreateMoveFromCommand(Move);
        }

        public IList<IPlayer> GetPlayers(int numberOfPlayers)
        {
            return new List<IPlayer>
            {
                new Player("Fermin", ChessColor.White),
                new Player("Bot", ChessColor.Black)
            };
        }
    }
}
