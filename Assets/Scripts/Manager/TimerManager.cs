using System;
using UnityEngine;

namespace Manager
{
    public class TimerManager : MonoBehaviour
    {
        private float _timer;
        public delegate void OnDelegateTimerStart();
        public delegate void OnDelegateTimerRunning(int t);
        public delegate void OnDelegateTimerEnd();

        public event OnDelegateTimerStart OnTimerStart;     
        public event OnDelegateTimerRunning OnTimerRunning;     
        public event OnDelegateTimerEnd OnTimerEnd;     
        private void Update()
        {
            if(_timer < 0)return;
            _timer -= Time.deltaTime * 1;
            OnOnTimerRunning((int)_timer);
            if (Math.Abs(_timer - 0) < 0.01)OnOnTimerEnd();
        }

        public void SetTimer(float t)
        {
            _timer = t;
            OnOnTimerStart();
        }

        protected virtual void OnOnTimerStart()
        {
            OnTimerStart?.Invoke();
        }

        protected virtual void OnOnTimerRunning(int t)
        {
            OnTimerRunning?.Invoke(t);
        }

        protected virtual void OnOnTimerEnd()
        {
            OnTimerEnd?.Invoke();
        }
    }
}