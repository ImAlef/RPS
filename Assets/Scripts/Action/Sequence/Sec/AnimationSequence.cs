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
    public class AnimationSequence : ISequence
    {
        [OdinSerialize]public List<CustomData> CustomData { get; set; }
        public async Task RunSequence(List<CustomData> customDataHandler)
        {
            var animator = CustomData.GetValue<Animator>("Animator");
            var state = CustomData.GetValue<IntClass>("State").Value;
            
            animator.SetInteger("State" , state);
            await Task.CompletedTask;
        }
    }
}