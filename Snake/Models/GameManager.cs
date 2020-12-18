using System.Collections.Generic;

namespace Snake_SB2020.Models
{
    public class GameManager
    {
        public static GameManager Instance { get; } = new GameManager();
        private GameManager() { _gameBoards = new List<GameBoard>(); }
        
        private List<GameBoard> _gameBoards;
        
        // Дефолтные параметры игры
        private static readonly Size _defaultBoardSize = new Size(20, 20);
        private static readonly int _defaultTurnTimeInMilliseconds = 3000;
        
        /// <summary>
        /// Создает новую игру с заданными параметрами
        /// </summary>
        /// <param name="boardSize">Размер поля</param>
        /// <param name="turnTimeInMilliseconds">Время одного хода в миллисекундах</param>
        public int CreateNewGameBoard(Size boardSize, int turnTimeInMilliseconds)
        {
            GameBoard gameboard = new GameBoard(boardSize, turnTimeInMilliseconds);
            _gameBoards.Add(gameboard); // могут ли спутаться индексы из-за параллельных запросов?
            return _gameBoards.Count - 1;
        }
        
        /// <summary>
        /// Возвращает игру по id
        /// </summary>
        public IGameBoard GetGameBoard(int id)
        {
            if (_gameBoards.Count <= id || id < 0)
                return null;
            GameBoard _gameBoard = _gameBoards[id];
            _gameBoard.CountTimeUntilNextTurnMiliseconds();
            return _gameBoard;
        }
        
        /// <summary>
        /// Создает новую игру без автоматического смены хода по таймеру
        /// </summary>
        /// <param name="boardSize">Размер игрового поля</param>
        public int CreateGameBoardWithNoTimer(Size boardSize)
        {
            GameBoard gameboard = new GameBoard(boardSize);
            _gameBoards.Add(gameboard);
            return _gameBoards.Count - 1;
        }
    }
}