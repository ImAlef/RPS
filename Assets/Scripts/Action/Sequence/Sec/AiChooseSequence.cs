using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Logic.Script.Holder;
using Manager;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine;
namespace Action.Sequence.Sec
{
    public class AiChooseSequence : ISequence
    {
        [OdinSerialize] public List<CustomData> CustomData { get; set; }

        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var holder = CustomData.GetValue<StringObjectHolder>("SKQ-Holder");
            List<string> chooses = new List<string>(holder.values);
            chooses.Remove("Null");
            var choose = chooses[Random.Range(0, chooses.Count)];
            customDataHandler.TrySetOrAdd("AiChoose", choose);
            var obj = CustomData.GetValue<GameObject>(choose);
            var spawnPos = CustomData.GetValue<GameObject>("SpawnPos");
            var spawnManager = CustomData.GetValue<SpawnManager>("SpawnManager");
            var ins = spawnManager.Spawn(obj, spawnPos.transform);
            customDataHandler.TrySetOrAdd("AiChooseObject" , ins);

            await Task.CompletedTask;
        }
    }
}