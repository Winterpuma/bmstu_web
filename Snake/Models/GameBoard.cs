using System;
using System.Collections.Generic;

namespace Snake.Models
{
    class GameBoard : IGameBoard
    {
        public int TurnNumber { get; private set; } = 0;
        public int TimeUntilNextTurnMiliseconds { get; private set; }
        private TimerManager _timerManager;

        public Size GameBoardSize { get; private set; }

        private SnakeDirection _snakeHeadDirection;
        public List<Coordinate> Snake { get; private set; }
        public List<Coordinate> Food { get; private set; }

        private Random _r = new Random();
        
        /// <summary>
        /// Создает игру с автоматической сменой хода по таймеру
        /// </summary>
        /// <param name="gameBoardSize"></param>
        /// <param name="turnTimeInMilliseconds"></param>
        public GameBoard(Size gameBoardSize, int turnTimeInMilliseconds)
        {
            Snake = new List<Coordinate>();
            Food = new List<Coordinate>();

            this.GameBoardSize = gameBoardSize;
            _timerManager = new TimerManager(turnTimeInMilliseconds, UpdateGameBoard);

            StartGame();
        }
        
        /// <summary>
        /// Инициализирует игру при старте
        /// </summary>
        private void StartGame()
        {
            Snake.Clear();
            Food.Clear();

            TurnNumber = 0;
            _snakeHeadDirection = new SnakeDirection() { Direction = EnumDirection.Top };

            int centerX = GameBoardSize.Width / 2;
            int centerY = GameBoardSize.Height / 2;

            SpawnSnakeBodyCell(centerX, centerY);
            SpawnSnakeBodyCell(centerX, centerY + 1);

            SpawnNewFood();

            _timerManager.StartTimer();
        }
        
        /// <summary>
        /// Обновляет игру при наступлении следующего хода
        /// </summary>
        private void UpdateGameBoard(object o)
        {
            TurnNumber++;
            int nextHeadX = Snake[0].X + _snakeHeadDirection.GetDirectionDX();
            int nextHeadY = Snake[0].Y + _snakeHeadDirection.GetDirectionDY();

            // Змейка врезалась в стенку
            if (nextHeadX == -1 || nextHeadX == GameBoardSize.Width ||
                nextHeadY == -1 || nextHeadY == GameBoardSize.Height)
            {
                StartGame();
                _timerManager.StartTimer();
                return;
            }

            Snake.Insert(0, new Coordinate(nextHeadX, nextHeadY)); // голова подвинулась

            int indexOfFood = IndexInCoords(Food, nextHeadX, nextHeadY);
            if (indexOfFood != -1) // змейка съела еду
            {
                Food.RemoveAt(indexOfFood);
                SpawnNewFood();
            }
            else // обычное перемещение змейки
            {
                Snake.RemoveAt(Snake.Count - 1);
            }

            _timerManager.ResetStopwatch();
        }
        
        /// <summary>
        /// Устанавливает новую клетку тела змейки в заданных координатах
        /// </summary>
        private void SpawnSnakeBodyCell(int x, int y)
        {
            Snake.Add(new Coordinate(x, y));
        }
        
        /// <summary>
        /// Изменяет направление движения змейки
        /// </summary>
        /// <returns>0 - успешно, -1 ошибка</returns>
        public int ChangeSnakeDir(SnakeDirection newDirection)
        {
            if (newDirection.IsDirectionOk() && !newDirection.IsDirectionsContrary(_snakeHeadDirection))
            {
                _snakeHeadDirection = newDirection;
                return 0;
            }

            return -1;
        }
        
        /// <summary>
        /// Возвращает текущее направление змейки
        /// </summary>
        public SnakeDirection GetCurrentSnakeDirection()
        {
            return _snakeHeadDirection;
        }
        
        /// <summary>
        /// Располагает новую клетку еды, на поле, не занятом змейкой
        /// </summary>
        private void SpawnNewFood()
        {
            bool gotFreeCoord = false;
            int randomX = 0;
            int randomY = 0;

            while (!gotFreeCoord)
            {
                randomX = _r.Next(0, GameBoardSize.Width);
                randomY = _r.Next(0, GameBoardSize.Height);
                
                if (IndexInCoords(Snake, randomX, randomY) == -1 && // клетка не занята змейкой
                    IndexInCoords(Food, randomX, randomY) == -1) // клетка не занята едой
                    gotFreeCoord = true;
            }

            Food.Add(new Coordinate(randomX, randomY));
        }
        
        /// <summary>
        /// Находит индекс вхождения в список координат
        /// </summary>
        /// <returns>-1 если список не содержит такого элемента</returns>
        private int IndexInCoords(List<Coordinate> coordinates, int x, int y)
        {
            Coordinate coordToCheck = new Coordinate(x, y);
            return coordinates.FindIndex(el => el.Equals(coordToCheck));
        }
        
        /// <summary>
        /// Обновляет время оставшееся до следующего хода
        /// </summary>
        public void CountTimeUntilNextTurnMiliseconds()
        {
            TimeUntilNextTurnMiliseconds = _timerManager.CountTimeUntilNextTurnMiliseconds();
        }
    }
}