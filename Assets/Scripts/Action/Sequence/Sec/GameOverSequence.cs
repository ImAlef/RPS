using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine.UI;
using Variable;

namespace Action.Sequence.Sec
{
    public class GameOverSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var playerScore = customDataHandler.GetValue<IntClass>("PlayerScore").Value;
            var aiScore = customDataHandler.GetValue<IntClass>("AiScore").Value;
            var text = CustomData.GetValue<Text>("Text");

            if (playerScore > aiScore)
            {
                PlayerWin(text, playerScore, aiScore);
            }
            else AiWin(text, playerScore, aiScore);

            await Task.CompletedTask;
        }

        private void PlayerWin(Text text, int playerScore, int aiScore)
        {
            text.text = $"Player Score : {playerScore} \n Enemy Score : {aiScore} \n Congratulations, You Won";
        }
        
        private void AiWin(Text text , int playerScore , int aiScore)
        {
            text.text = $"Player Score : {playerScore} \n Enemy Score : {aiScore} \n Unfortunately, you lost";
        }
    }
}