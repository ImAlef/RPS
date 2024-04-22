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
    public class ChangeTextSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var text = CustomData.GetValue<Text>("Text");
            var mess = CustomData.GetValue<StringClass>("Message").Value;

            text.text = mess;
            await Task.CompletedTask;
        }
    }
}