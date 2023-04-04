using System;
using UnityEngine;

namespace PixelCrew.Timer
{
    [Serializable]
    public class Timer
    {
        public float _timer;
        private float _timerEnd;
        public bool checkTimer => Time.time >= _timerEnd;
        public void Reset()
        {
            _timerEnd = Time.time + _timer;
        }
    }
}