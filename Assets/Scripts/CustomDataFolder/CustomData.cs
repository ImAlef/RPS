using Sirenix.Serialization;

namespace CustomDataFolder
{
    public class CustomData
    {
        public string key;
        [OdinSerialize]public object value;
        
        public CustomData(string key, object objValue)
        {
            this.key = key;
            this.value = objValue;
        }
    }
}