using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Logic.Script.Game;
using Logic.Script.Window;
using Manager;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Variable;

namespace Action.Sequence.Sec
{
    public class CalculateScoreSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }

        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var scoreDatabase = CustomData.GetValue<MatrixDatabase>("ScoreDatabase");
            var playerChoose = customDataHandler.GetValue<string>("PlayerChoose");
            var aiChoose = customDataHandler.GetValue<string>("AiChoose");
            var txt = CustomData.GetValue<Text>("Text");

            var result = scoreDatabase.GetResult(new BaseMatrixSlot(aiChoose), new BaseMatrixSlot(playerChoose));
            switch (result)
            {
                case 0:
                    txt.gameObject.SetActive(true);
                    txt.text = "Draw";
                    await Task.Delay(1000);
                    txt.text = "";
                    txt.gameObject.SetActive(false);
                    await Reset(customDataHandler);

                    break;
                case 1:
                    await PlayerWin(customDataHandler, txt);
                    break;
                case -1 :
                    await AiWin(customDataHandler, txt);
                    break;
            }
        }

        private async Task AiWin(List<CustomData> customDataHandler, Text txt)
        {
            customDataHandler.TrySetOrAdd("Winner", "Ai");
            txt.gameObject.SetActive(true);
            txt.text = "EnemyWin";
            
            var aiScore = customDataHandler.GetValue<IntClass>("AiScore").Value;
            var enemyWin = CustomData.GetValue<GameObject>("Enemy-WinColor");
            var playerLose = CustomData.GetValue<GameObject>("Player-LoseColor");
            var round = CustomData.GetValue<IntClass>("Round").Value;
            
            customDataHandler.TrySetOrAdd("AiScore" , new IntClass(){Value = (aiScore+1)});
            enemyWin.SetActive(true);
            playerLose.SetActive(true);

            await Task.Delay(1000);
            
            enemyWin.SetActive(false);
            playerLose.SetActive(false);

            if (aiScore >= round)
            {
                var gameManager = CustomData.GetValue<GameManager>("GameManager");
                gameManager.RunAction("GameOver");
                txt.gameObject.SetActive(false);
                await Task.CompletedTask;
                return;
            }
            txt.text = "";
            txt.gameObject.SetActive(false);
            await Reset(customDataHandler);
        }

        private async Task PlayerWin(List<CustomData> customDataHandler, Text txt)
        {
            customDataHandler.TrySetOrAdd("Winner", "Player");
            txt.gameObject.SetActive(true);
            txt.text = "PlayerWin";
            
            var playerScore = customDataHandler.GetValue<IntClass>("PlayerScore").Value;
            var enemyLose = CustomData.GetValue<GameObject>("Enemy-LoseColor");
            var playerWin = CustomData.GetValue<GameObject>("Player-WinColor");
            var round = CustomData.GetValue<IntClass>("Round").Value;
            
            customDataHandler.TrySetOrAdd("PlayerScore" , new IntClass(){Value = (playerScore+1)});
            enemyLose.SetActive(true);
            playerWin.SetActive(true);

            await Task.Delay(1000);
            
            enemyLose.SetActive(false);
            playerWin.SetActive(false);

            if (playerScore >= round)
            {
                var gameManager = CustomData.GetValue<GameManager>("GameManager");
                gameManager.RunAction("GameOver");
                txt.gameObject.SetActive(false);
                await Task.CompletedTask;
                return;
            }

            txt.text = "";
            txt.gameObject.SetActive(false);
            await Reset(customDataHandler);
        }

        public async Task Reset(List<CustomData> customDataHandler)
        {
            var gameManager = CustomData.GetValue<GameManager>("GameManager");
            var timerManager = CustomData.GetValue<TimerManager>("TimerManager");
            gameManager.OnChangeScoreEvent();
            var playerObj = customDataHandler.GetValue<GameObject>("PlayerChooseObject");
            var aiObj = customDataHandler.GetValue<GameObject>("AiChooseObject");
            timerManager.SetTimer(3);
            await Task.Delay(3000);

            playerObj.GetComponent<Animator>().SetInteger("State" , 1);
            aiObj.GetComponent<Animator>().SetInteger("State" , 1);
            await Task.Delay(1000);
            
            gameManager.DestroyFunction(playerObj);
            gameManager.DestroyFunction(aiObj);
        }
        
    }
}