using Logic.Script.Game;
using UnityEngine;

namespace Logic.Script.Holder
{
    public abstract class HolderObject : ScriptableObject
    {
        public abstract object[] Objects { get; set; }
        public abstract BaseMatrixSlot ConvertToMatrixSlot(int i);

        public abstract int CheckCount();

        public abstract void CreateSort();

        public abstract BaseMatrixSlot ConvertToSlot(int i);
        public abstract int CheckCountWithoutChild();
    }
}