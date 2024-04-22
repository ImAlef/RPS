using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Manager;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine;
using Variable;

namespace Action.Sequence.Sec
{
    public class SpawnSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var idToCustomData = CustomData.GetValue<StringClass>("IdToCustomData").Value;
            var spawnPos = CustomData.GetValue<GameObject>("SpawnPos");
            var obj = CustomData.GetValue<GetGameObjectOfListCustomDataWithString>("SpawnObject").GetValue();
            var spawnManager = CustomData.GetValue<SpawnManager>("SpawnManager");
            
            var ins = spawnManager.Spawn(obj , spawnPos.transform);
            customDataHandler.TrySetOrAdd(idToCustomData , ins);
            await Task.CompletedTask;
        }
    }
}