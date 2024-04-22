using Logic.Script.Game;
using UnityEngine;

namespace Logic.Script.Window
{
    public interface IMatrixValue
    {
        public Vector2 Key { get; set; }
        public int Result { get; set; }
        public BaseMatrixSlot Ability { get; set; }
    }
}