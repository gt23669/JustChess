namespace JustChess.Engine.Contracts
{
    using System.Collections.Generic;
    using JustChess.Players.Contracts;

    public interface IChessEngine
    {
        IEnumerable<IPlayer> Players { get; }

        void Initialize(IGameInitializationStrategy gameInitializationStrategy, bool chess960);

        void Start();

        void WinningConditions();
    }
}
