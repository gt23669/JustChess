namespace JustChess.Engine.Initializations
{
    using System;
    using System.Collections.Generic;

    using Board.Contracts;
    using Common;
    using Contracts;
    using Figures;
    using Figures.Contracts;
    using Players.Contracts;

    public class StandardStartGameInitializationStrategy : IGameInitializationStrategy
    {
        private const int BoardTotalRowsAndCols = 8;

        private IList<Type> figureTypes;

        public StandardStartGameInitializationStrategy()
        {
            this.figureTypes = new List<Type>
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

        public void Initialize(IList<IPlayer> players, IBoard board, bool chess960)
        {
            this.ValidateStrategy(players, board);

            var firstPlayer = players[0];
            var secondPlayer = players[1];

            this.AddArmyToBoardRow(firstPlayer, board, 8, chess960);
            this.AddPawnsToBoardRow(firstPlayer, board, 7);

            this.AddPawnsToBoardRow(secondPlayer, board, 2);
            this.AddArmyToBoardRow(secondPlayer, board, 1, chess960);
        }

        private void AddPawnsToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            for (int i = 0; i < BoardTotalRowsAndCols; i++)
            {
                var pawn = new Pawn(player.Color);
                player.AddFigure(pawn);
                var position = new Position(chessRow, (char)(i + 'a'));
                board.AddFigure(pawn, position);
            }
        }

        private void AddArmyToBoardRow(IPlayer player, IBoard board, int chessRow, bool chess960)
        {
            if (chess960)
            {
                RandomizeList();
            }
            for (int i = 0; i < BoardTotalRowsAndCols; i++)
            {
                var figureType = this.figureTypes[i];
                var figureInstance = (IFigure)Activator.CreateInstance(figureType, player.Color);
                player.AddFigure(figureInstance);
                var position = new Position(chessRow, (char)(i + 'a'));
                board.AddFigure(figureInstance, position);
            }
        }

        private void RandomizeList()
        {
            Type[] temp = new Type[8];
            Random rnd = new Random();
            int left = rnd.Next(0, 3);
            int right = rnd.Next(5, 7);
            int kingIndex = rnd.Next(left + 1, right - 1);

            temp[left] = typeof(Rook);
            temp[right] = typeof(Rook);

            for (int i = 0; i < this.figureTypes.Count; i++)
            {
                if (this.figureTypes[i] == typeof(Rook))
                {
                    this.figureTypes.RemoveAt(i);
                }
            }

            for (int i = 0, j = 0; i < this.figureTypes.Count; i++)
            {
                while (temp[j] != null)
                {
                    j++;
                }
                temp[j] = figureTypes[i];
            }

            this.figureTypes = new List<Type>(temp);
        }

        private void ValidateStrategy(ICollection<IPlayer> players, IBoard board)
        {
            if (players.Count != GlobalConstants.StandardGameNumberOfPlayers)
            {
                throw new InvalidOperationException("Standard Start Game Initialization Strategy needs exactly two players!");
            }

            if (board.TotalRows != BoardTotalRowsAndCols || board.TotalCols != BoardTotalRowsAndCols)
            {
                throw new InvalidOperationException("Standard Start Game Initialization Strategy needs 8x8 board!");
            }
        }
    }
}
