using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine;
using Variable;

namespace Action.Sequence.Sec
{
    public class ActiveObjectSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var obj = CustomData.GetValue<GameObject>("Obj");
            var boolValue = CustomData.GetValue<BoolClass>("Active").Value;

            obj.SetActive(boolValue);
            await Task.CompletedTask;
        }
    }
}