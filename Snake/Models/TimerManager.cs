using System;
using System.Diagnostics;
using System.Threading;

namespace Snake.Models
{
    /// <summary>
    /// Отвечает за запуск функции по таймеру и 
    /// взаимодействие со временем связанным с таймером
    /// </summary>
    public class TimerManager
    {
        private bool _timeoutTypeManager = true;

        private TimeSpan _turnTime;
        private Timer _onUpdateTimer;        
        private Stopwatch _stopwatch;
        
        /// <summary>
        /// Менеджер для периодично запускаемой function
        /// </summary>
        /// <param name="turnTimeInMilliseconds">Периодичность запуска</param>
        /// <param name="function">Периодично запускаема функция</param>
        public TimerManager(int turnTimeInMilliseconds, TimerCallback function)
        {
            _turnTime = new TimeSpan(0, 0, 0, 0, turnTimeInMilliseconds);
            _onUpdateTimer = new Timer(function);
            _stopwatch = new Stopwatch();
        }
        
        /// <summary>
        /// Менеджер для function, вызываемой из кода, а не по таймеру
        /// </summary>
        /// <param name="function"></param>
        public TimerManager()
        {
            _timeoutTypeManager = false;
        }
        
        /// <summary>
        /// Возвращает true, если TimerManager установлен в режим работы по таймауту
        /// </summary>
        public bool IsTimeoutType()
        {
            return _timeoutTypeManager;
        }
        
        /// <summary>
        /// Запуск таймера
        /// </summary>
        public void StartTimer()
        {
            if (_timeoutTypeManager)
            {
                _stopwatch.Reset();
                _stopwatch.Start();
                _onUpdateTimer.Change(0, (int)_turnTime.TotalMilliseconds); // запуск периодичного function
            }
        }
        
        /// <summary>
        /// Перезапуск часов
        /// </summary>
        public void ResetStopwatch()
        {
            if (_timeoutTypeManager)
            {
                _stopwatch.Reset();
                _stopwatch.Start();
            }
        }
        
        /// <summary>
        /// Возвращает время до наступления следующего хода
        /// </summary>
        public int CountTimeUntilNextTurnMiliseconds()
        {
            if (_timeoutTypeManager)
            {
                TimeSpan remainingTime = _turnTime.Subtract(_stopwatch.Elapsed);
                return (int)remainingTime.TotalMilliseconds;
            }
            else
            {
                return -1;
            }
        }
    }
}