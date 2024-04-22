using System.Collections.Generic;
using Action.Sequence.Interface;
using CustomDataFolder;
using Script.CustomDataFolder;
using Sirenix.Serialization;

namespace Script.Action.Action
{
    public class BaseAction : IAction
    {
        public string Id { get => id; set => id = value; }
        public string id;

        [OdinSerialize]public ISequence[] Sequences { get; set; }

        public async void Run(List<CustomData> customData)
        {
            foreach (var sequence in Sequences)
            {
                await sequence.RunSequence(customData);
            }
        }
    }
}