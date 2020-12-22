using System;

namespace Snake.Models
{
    public interface IGameManager
    {
        Guid CreateNewGameBoard(Size boardSize, int turnTimeInMilliseconds);
        IGameBoard GetGameBoard(Guid id);
    }
}
