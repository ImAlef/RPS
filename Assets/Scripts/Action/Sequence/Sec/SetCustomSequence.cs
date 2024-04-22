using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Script.Ext;
using Sirenix.Serialization;
using Variable;

namespace Action.Sequence.Sec
{
    public class SetCustomSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var value = CustomData.GetValue<object>("Value");
            var key = CustomData.GetValue<StringClass>("Key").Value;
            
            customDataHandler.TrySetOrAdd(key , value);
            await Task.CompletedTask;
        }
    }
}