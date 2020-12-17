namespace Snake_SB2020.Models
{
    public static class GameManager
    {
        private static object _lock = new object();
        private static GameBoard _gameBoard;
        
        // Дефолтные параметры игры
        private static readonly Size _defaultBoardSize = new Size(20, 20);
        private static readonly int _defaultTurnTimeInMilliseconds = 3000;
        
        /// <summary>
        /// Создает новую игру с заданными параметрами
        /// </summary>
        /// <param name="boardSize">Размер поля</param>
        /// <param name="turnTimeInMilliseconds">Время одного хода в миллисекундах</param>
        public static IGameBoard CreateNewGameBoard(Size boardSize, int turnTimeInMilliseconds)
        {
            _gameBoard = new GameBoard(boardSize, turnTimeInMilliseconds);

            return _gameBoard;
        }
        
        /// <summary>
        /// Возвращает текущую игру или создает новую, если ее не существует
        /// </summary>
        public static IGameBoard GetGameBoard()
        {
            if (_gameBoard == null)
                lock(_lock)
                    if (_gameBoard == null)
                        _gameBoard = (GameBoard)CreateNewGameBoard(_defaultBoardSize, _defaultTurnTimeInMilliseconds);

            _gameBoard.CountTimeUntilNextTurnMiliseconds();
            return _gameBoard;
        }
        
        /// <summary>
        /// Создает новую игру без автоматического смены хода по таймеру
        /// </summary>
        /// <param name="boardSize">Размер игрового поля</param>
        public static IGameBoard CreateGameBoardWithNoTimer(Size boardSize)
        {
            _gameBoard = new GameBoard(boardSize);

            return _gameBoard;
        }
    }
}