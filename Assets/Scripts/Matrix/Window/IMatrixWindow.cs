using Logic.Script.Game;
using UnityEngine;

namespace Logic.Script.Window
{
    public interface IMatrixWindow
    {
        public void ChangeValue(BaseMatrixSlot slot);
        public void ChangeValueCondition(BaseMatrixSlot slot);
        public void ChangeValueVertical(BaseMatrixSlot slot);
        public void ChangeValueVerticalCondition(BaseMatrixSlot slot);
        public Vector2 GetLocation(BaseMatrixSlot slot);
    }
}