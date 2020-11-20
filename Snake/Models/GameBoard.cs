using System;
using System.Collections.Generic;

namespace Snake_SB2020.Models
{
    public class GameBoard
    {
        public int turnNumber = 0;
        public int timeUntilNextTurnMiliseconds;
        private TimerManager _timerManager;

        public Size gameBoardSize;

        private SnakeDirection _snakeHeadDirection;
        public List<Coordinate> snake;
        public List<Coordinate> food;

        private Random _r = new Random();
        
        /// <summary>
        /// Создает игру с автоматической сменой хода по таймеру
        /// </summary>
        /// <param name="gameBoardSize"></param>
        /// <param name="turnTimeInMilliseconds"></param>
        public GameBoard(Size gameBoardSize, int turnTimeInMilliseconds)
        {
            snake = new List<Coordinate>();
            food = new List<Coordinate>();

            this.gameBoardSize = gameBoardSize;
            _timerManager = new TimerManager(turnTimeInMilliseconds, UpdateGameBoard);

            StartGame();
        }
        
        /// <summary>
        /// Создает игру без автоматической смены хода по таймеру
        /// </summary>
        /// <param name="gameBoardSize">Размер поля</param>
        public GameBoard(Size gameBoardSize)
        {
            snake = new List<Coordinate>();
            food = new List<Coordinate>();

            this.gameBoardSize = gameBoardSize;
            _timerManager = new TimerManager();

            StartGame();
        }
        
        /// <summary>
        /// Инициализирует игру при старте
        /// </summary>
        private void StartGame()
        {
            snake.Clear();
            food.Clear();

            turnNumber = 0;
            _snakeHeadDirection = new SnakeDirection() { Direction = "Top" };

            int centerX = gameBoardSize.Width / 2;
            int centerY = gameBoardSize.Height / 2;

            SpawnSnakeBodyCell(centerX, centerY);
            SpawnSnakeBodyCell(centerX, centerY + 1);

            SpawnNewFood();

            _timerManager.StartTimer();
        }
        
        /// <summary>
        /// Принудительная смена хода.
        /// Возможна только если не установлен ход по таймеру.
        /// </summary>
        /// <returns>0 - успех, -1 - ошибка</returns>
        public int ForcedNextMove()
        {
            if (_timerManager.IsTimeoutType()) // установлен ход по таймеру
                return -1;

            UpdateGameBoard("");
            return 0;
        }
        
        /// <summary>
        /// Обновляет игру при наступлении следующего хода
        /// </summary>
        private void UpdateGameBoard(object o)
        {
            turnNumber++;
            int nextHeadX = snake[0].X + _snakeHeadDirection.GetDirectionDX();
            int nextHeadY = snake[0].Y + _snakeHeadDirection.GetDirectionDY();

            // Змейка врезалась в стенку
            if (nextHeadX == -1 || nextHeadX == gameBoardSize.Width ||
                nextHeadY == -1 || nextHeadY == gameBoardSize.Height)
            {
                StartGame();
                _timerManager.StartTimer();
                return;
            }

            snake.Insert(0, new Coordinate(nextHeadX, nextHeadY)); // голова подвинулась

            int indexOfFood = IndexInCoords(food, nextHeadX, nextHeadY);
            if (indexOfFood != -1) // змейка съела еду
            {
                food.RemoveAt(indexOfFood);
                SpawnNewFood();
            }
            else // обычное перемещение змейки
            {
                snake.RemoveAt(snake.Count - 1);
            }

            _timerManager.ResetStopwatch();
        }
        
        /// <summary>
        /// Устанавливает новую клетку тела змейки в заданных координатах
        /// </summary>
        private void SpawnSnakeBodyCell(int x, int y)
        {
            snake.Add(new Coordinate(x, y));
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
                randomX = _r.Next(0, gameBoardSize.Width);
                randomY = _r.Next(0, gameBoardSize.Height);
                
                if (IndexInCoords(snake, randomX, randomY) == -1 && // клетка не занята змейкой
                    IndexInCoords(food, randomX, randomY) == -1) // клетка не занята едой
                    gotFreeCoord = true;
            }

            food.Add(new Coordinate(randomX, randomY));
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
            timeUntilNextTurnMiliseconds = _timerManager.CountTimeUntilNextTurnMiliseconds();
        }
    }
}