using JustChess.Common;
using JustChess.InputProviders.Contracts;
using JustChess.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.InputProviders
{
    public class MockInputProvider : IInputProvider
    {
        public Move GetNextPlayerMove(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public IList<IPlayer> GetPlayers(int numberOfPlayers)
        {
            throw new NotImplementedException();
        }
    }
}
