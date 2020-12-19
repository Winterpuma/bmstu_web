using System.Collections.Concurrent;
using System;

namespace Snake_SB2020.Models
{
    public class GameManager
    {
        public static GameManager Instance { get; } = new GameManager();
        private GameManager() { _gameBoards = new ConcurrentDictionary<Guid, GameBoard>(); }
        
        private ConcurrentDictionary<Guid, GameBoard> _gameBoards;
        
        // Дефолтные параметры игры
        private static readonly Size _defaultBoardSize = new Size(20, 20);
        private static readonly int _defaultTurnTimeInMilliseconds = 3000;
        
        /// <summary>
        /// Создает новую игру с заданными параметрами
        /// </summary>
        /// <param name="boardSize">Размер поля</param>
        /// <param name="turnTimeInMilliseconds">Время одного хода в миллисекундах</param>
        public Guid CreateNewGameBoard(Size boardSize, int turnTimeInMilliseconds)
        {
            GameBoard gameboard = new GameBoard(boardSize, turnTimeInMilliseconds);
            Guid id = Guid.NewGuid();//.ToString("N");
            _gameBoards.TryAdd(id, gameboard);
            return id;
        }
        
        /// <summary>
        /// Возвращает игру по id
        /// </summary>
        public IGameBoard GetGameBoard(Guid id)
        {
            GameBoard _gameBoard;
            _gameBoards.TryGetValue(id, out _gameBoard);
            if (_gameBoard != null)
                _gameBoard.CountTimeUntilNextTurnMiliseconds();
            return _gameBoard;
        }
        
        /// <summary>
        /// Создает новую игру без автоматического смены хода по таймеру
        /// </summary>
        /// <param name="boardSize">Размер игрового поля</param>
        public Guid CreateGameBoardWithNoTimer(Size boardSize)
        {
            GameBoard gameboard = new GameBoard(boardSize);
            //string id = Guid.NewGuid().ToString("N");
            Guid id = Guid.NewGuid();
            _gameBoards.TryAdd(id, gameboard);
            return id;
        }
    }
}