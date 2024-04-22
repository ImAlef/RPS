using Logic.Script.Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Script.Holder
{
    [CreateAssetMenu(menuName = "Holder/strHolder" , fileName = "Holder")]
    public class StringObjectHolder : HolderObject
    {
        public override object[] Objects { get => values; set => values = (string[])value; }
        [FormerlySerializedAs("fazes")] public string[] values;
        public override BaseMatrixSlot ConvertToMatrixSlot(int i)
        {
            return new BaseMatrixSlot(values[i]);
        }

        public override int CheckCount()
        {
            return values.Length;
        }

        public override void CreateSort()
        {
            
        }

        public override BaseMatrixSlot ConvertToSlot(int i)
        {
            return new BaseMatrixSlot(values[i]);
        }

        public override int CheckCountWithoutChild()
        {
           return values.Length;
        }
    }
}