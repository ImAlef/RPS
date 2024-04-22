using System;
using Logic.Script.Game;
using UnityEngine;

namespace Logic.Script.Window
{
    [Serializable]
    public class MatrixValue : IMatrixValue
    {
        public Vector2 Key { get=>key; set=>key = value; }
        public Vector2 key;
        public int Result { get=>result; set=>result=value; }
        public int result;
        public BaseMatrixSlot Ability { get; set; }
        public MatrixValue(Vector2 key, int result)
        {
            Key = key;
            Result = result;
        }
    }
}