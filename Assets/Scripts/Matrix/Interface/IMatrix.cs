using System.Collections.Generic;

namespace Logic.Script.Game.Interface
{
    public interface IMatrix
    {
        string Tag { get; set; }
        List<string> Location { get; set; }
    }
}