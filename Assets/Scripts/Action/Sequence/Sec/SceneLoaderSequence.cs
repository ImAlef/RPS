using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine.SceneManagement;
using Variable;

namespace Action.Sequence.Sec
{
    public class SceneLoaderSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var sceneId = CustomData.GetValue<StringClass>("SceneId").Value;
            SceneManager.LoadScene(sceneId);
            await Task.CompletedTask;
        }
    }
}