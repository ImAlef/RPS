using System.Collections.Generic;
using Logic.Script.Game;
using UnityEngine;

namespace Logic.Script.Holder
{
    [CreateAssetMenu(menuName = "Holder/SpriteHolder", fileName = "New SpriteHolder ")]
    public class SpriteHolder : HolderObject
    {
        private List<BaseMatrixSlot> _matrices;
        public override object[] Objects { get => objects; set => objects = (Sprite[])value; }
        public Sprite[] objects;
        public override BaseMatrixSlot ConvertToMatrixSlot(int i)
        {
            return new BaseMatrixSlot(objects[i].name);
        }

        public override int CheckCount()
        {
            return objects.Length;
        }

        public override void CreateSort()
        {
            _matrices = new List<BaseMatrixSlot>();
            foreach (var obj in objects)
            {
                _matrices.Add(new BaseMatrixSlot(obj.ToString()));
            }
        }

        public override BaseMatrixSlot ConvertToSlot(int i)
        {
            return ConvertToMatrixSlot(i);
        }

        public override int CheckCountWithoutChild()
        {
            return objects.Length;
        }
    }
}