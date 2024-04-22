using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        public Text playerScore;
        public Text aiScore;
        public GameManager gameManager;

        private void OnEnable()
        {
            gameManager.OnChangeScore += OnChangeScoreCallBack;
        }

        private void OnChangeScoreCallBack(int p, int a)
        {
            playerScore.text = p.ToString();
            aiScore.text = a.ToString();
        }
    }
}