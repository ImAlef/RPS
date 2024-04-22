using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Sequence.Interface;
using CustomDataFolder;
using Manager;
using Script.CustomDataFolder;
using Script.Ext;
using Sirenix.Serialization;
using Variable;

namespace Action.Sequence.Sec
{
    public class TimeDelayUISequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var time = CustomData.GetValue<IntClass>("Timer").Value;
            var timeManager = CustomData.GetValue<TimerManager>("TimerManager");
            timeManager.SetTimer(time);
            await Task.Delay(time * 1000);
        }
    }
}