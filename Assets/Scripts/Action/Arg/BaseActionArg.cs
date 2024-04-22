using System.Collections.Generic;

namespace Script.Action.Arg
{
    public class BaseActionArg : IActionArg
    {
        public List<ArgValue> ArgValues;

        public BaseActionArg(List<ArgValue> argValues)
        {
            ArgValues = argValues;
        }
    }
}