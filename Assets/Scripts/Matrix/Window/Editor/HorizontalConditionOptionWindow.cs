using Logic.Script.Game;
using Logic.Script.Holder;
using UnityEditor;
using UnityEngine;

namespace Logic.Script.Window
{
    public class HorizontalConditionOptionWindow : EditorWindow
    {
        public int[] matrixInt;
        public int size = 50;
        public BaseMatrixSlot selectedSlot;
        private HolderObject columnObject;
        private BaseMatrixSlot[] slots;
        private IMatrixWindow _matrixCreator;
        private HolderObject _rowsObject;
        private Vector2 scrollPosition;
        private float f = 0.5f;
        Vector2 verticalScrollPosition = Vector2.zero;


        public void SetMatrixSlot(BaseMatrixSlot slot, HolderObject column, IMatrixWindow matrixCreator,
            int[,] matrixB, HolderObject rows)
        {
            selectedSlot = slot;
            columnObject = column;
            _rowsObject = rows;
            _matrixCreator = matrixCreator;
            slots = new BaseMatrixSlot[_rowsObject.Objects.Length];
            matrixInt = new int[_rowsObject.Objects.Length];
            var vec = _matrixCreator.GetLocation(selectedSlot);
            for (int i = 0; i < rows.Objects.Length; i++)
            {
                for (int j = 0; j < columnObject.Objects.Length; j++)
                {
                    if (vec.y == j)
                    {
                        matrixInt[i] = matrixB[i, j];
                    }
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = _rowsObject.ConvertToSlot(i);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Confirm"))
            {
                ChangeMatrixResult();
                Close();
            }

            if (GUILayout.Button("SelectAll"))
            {
                for (int i = 0; i < matrixInt.Length; i++)
                {
                    matrixInt[i] = 1;
                }
            }

            if (GUILayout.Button("DeSelectAll"))
            {
                for (int i = 0; i < matrixInt.Length; i++)
                {
                    matrixInt[i] = 0;
                }
            }

            
            GUILayout.Label("You Can bit Other's Ability");

            
            float windowHeight = position.height; // ارتفاع صفحه
            windowHeight -= 200;
            float windowWidth = position.width; // ارتفاع صفحه
            windowWidth -= 200;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(windowHeight));
            verticalScrollPosition = EditorGUILayout.BeginScrollView(verticalScrollPosition,
                GUILayout.Width(windowWidth), GUILayout.Height(windowHeight));
            Rect matrixRect = EditorGUILayout.GetControlRect(false, 2 * size,
                GUILayout.Width(columnObject.CheckCount() * size));
            
            for (var i = 0; i < 2; i++)
            {
                GUILayout.BeginVertical();

                for (var j = 0; j < _rowsObject.Objects.Length; j++)
                {
                    var cellRect = new Rect(j * size, i * size, size, size);
                    EditorGUI.DrawRect(cellRect, Color.gray);

                    switch (i)
                    {
                        case 0:
                            GUI.TextArea(cellRect, slots[j].Tag);
                            break;
                        case 1 when j == 0:
                            GUI.TextArea(cellRect, selectedSlot.Tag);
                            break;
                    }

                    if (i != 1) continue;
                    if (j != 0)
                    {
                        matrixInt[j] = EditorGUI.IntField(cellRect, matrixInt[j]);
                    }
                }

                GUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndScrollView();
           
        }

        public int GetResult(int b)
        {
            return matrixInt[b];
        }

        private void ChangeMatrixResult()
        {
            _matrixCreator.ChangeValueCondition(selectedSlot);
        }
    }
}
