using Logic.Script.Game;
using UnityEngine;

namespace Logic.Script.Window
{
    [System.Serializable]
    public class MatrixObject : IMatrixValue
    {
        public MatrixObject(Vector2 key, BaseMatrixSlot ability)
        {
            Key = key;
            Ability = ability;
        }


        public Vector2 Key { get=>key; set=>key = value; }
        public Vector2 key;     
        public int Result { get; set; }
        public BaseMatrixSlot Ability { get=>ability; set=>ability =value; }
        public BaseMatrixSlot ability;
    }
}