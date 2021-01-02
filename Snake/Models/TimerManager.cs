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
        /// Запуск таймера
        /// </summary>
        public void StartTimer()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            _onUpdateTimer.Change(0, (int)_turnTime.TotalMilliseconds); // запуск периодичного function
        }

        /// <summary>
        /// Перезапуск часов
        /// </summary>
        public void ResetStopwatch()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        /// <summary>
        /// Возвращает время до наступления следующего хода
        /// </summary>
        public int CountTimeUntilNextTurnMiliseconds()
        {
            TimeSpan remainingTime = _turnTime.Subtract(_stopwatch.Elapsed);
            return (int)remainingTime.TotalMilliseconds;
        }
    }
}