using System.Collections.Generic;
using System.Threading.Tasks;
using CustomDataFolder;
using Script.CustomDataFolder;
using Sirenix.Serialization;

namespace Action.Sequence.Interface
{
    public interface ISequence
    {
        [OdinSerialize]List<CustomData> CustomData { get; set; }
        Task RunSequence(List<CustomData> customDataHandler);
    }
}