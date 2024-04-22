using System;

namespace Script.Action.Arg
{
    [Serializable]
    public class ArgValue
    {
        public string id;
        public object Value;

        public ArgValue(string id, object value)
        {
            this.id = id;
            Value = value;
        }
    }
}