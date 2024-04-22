using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeManagerUI : MonoBehaviour
    {
        public TimerManager timerManager;
        public GameObject timer;
        public Text txt;
        private void OnEnable()
        {
            timerManager.OnTimerStart += OnTimerStartCallBack;
            timerManager.OnTimerEnd += OnTimerEndCallBack;
            timerManager.OnTimerRunning += OnTimerRunningCallBack;
        }

        private void OnTimerRunningCallBack(int t)
        {
            txt.text = t.ToString();
        }

        private void OnTimerEndCallBack()
        {
            timer.SetActive(false);
        }

        private void OnTimerStartCallBack()
        {
            timer.SetActive(true);
        }
    }
}