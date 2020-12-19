using System.Collections.Generic;

namespace Snake.Models
{
    public interface IGameBoard
    {
        int TurnNumber { get; }
        int TimeUntilNextTurnMiliseconds { get; }

        Size GameBoardSize { get; }
        
        List<Coordinate> Snake { get; }
        List<Coordinate> Food { get; }

        int ChangeSnakeDir(SnakeDirection newDirection);
    }
}
