using System.Collections.Generic;
using Action.Sequence.Interface;
using CustomDataFolder;
using Script.Action.Arg;
using Script.CustomDataFolder;

namespace Script.Action.Action
{
    public interface IAction
    {
        string Id { get; set; }
        
        ISequence[] Sequences { get; set; }

        void Run(List<CustomData> customData);
    }
}