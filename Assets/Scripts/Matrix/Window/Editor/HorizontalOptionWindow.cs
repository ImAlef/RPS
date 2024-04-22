using System;
using Logic.Script.Game;
using Logic.Script.Holder;
using UnityEditor;
using UnityEngine;

namespace Logic.Script.Window
{
    public class HorizontalOptionWindow : EditorWindow
    {
        public bool[] matrixBool;
        public int size = 50;
        public BaseMatrixSlot selectedSlot;
        private HolderObject columnObject;
        private BaseMatrixSlot[] slots;
        private IMatrixWindow _matrixCreator;
        private HolderObject _rowsObject;
        private Vector2 scrollPosition;
        private Vector2 verticalScrollPosition = Vector2.zero;
        private float f = 0.5f;

        public void SetMatrixSlot(BaseMatrixSlot slot, HolderObject column, IMatrixWindow matrixCreator,
            int[,] matrixB, HolderObject rows)
        {
            selectedSlot = slot;
            columnObject = column;
            _rowsObject = rows;
            _matrixCreator = matrixCreator;
            slots = new BaseMatrixSlot[columnObject.CheckCount()];
            matrixBool = new bool[columnObject.CheckCount()];
            var vec = _matrixCreator.GetLocation(selectedSlot);
            for (int i = 0; i < rows.CheckCount(); i++)
            {
                for (int j = 0; j < columnObject.CheckCount(); j++)
                {
                    if (vec.y == j)
                    {
                        matrixBool[i] = Convert.ToBoolean(matrixB[i, j]);
                    }
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = columnObject.ConvertToMatrixSlot(i);
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
                for (int i = 0; i < matrixBool.Length; i++)
                {
                    matrixBool[i] = true;
                }
            }

            if (GUILayout.Button("DeSelectAll"))
            {
                for (int i = 0; i < matrixBool.Length; i++)
                {
                    matrixBool[i] = false;
                }
            }

            GUILayout.Label("You Can Defuse Other's Ability");

            // ایجاد اسکرول‌بار افقی در بالای صفحه
            float windowHeight = position.height; // ارتفاع صفحه
            windowHeight -= 200;
            float windowWidth = position.width; // ارتفاع صفحه
            windowWidth -= 200;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(windowHeight));
            verticalScrollPosition = EditorGUILayout.BeginScrollView(verticalScrollPosition,
                GUILayout.Width(windowWidth), GUILayout.Height(windowHeight));

            Rect matrixRect = EditorGUILayout.GetControlRect(false, 2 * size,
                GUILayout.Width(columnObject.CheckCount() * size));
            //GUILayout.BeginHorizontal();
            
            for (int i = 0; i < 2; i++)
            {
                GUILayout.BeginVertical();

                for (int j = 0; j < columnObject.CheckCount(); j++)
                {
                    Rect cellRect = new Rect(j * size, i * size, size, size);
                    EditorGUI.DrawRect(cellRect, Color.gray);

                    if (i == 0)
                    {
                        GUI.TextArea(cellRect, slots[j].Tag);
                    }

                    if (i == 1 && j == 0)
                    {
                        GUI.TextArea(cellRect, selectedSlot.Tag);
                    }

                    if (i == 1)
                    {
                        if (j != 0)
                        {
                            matrixBool[j] = EditorGUI.Toggle(cellRect, matrixBool[j]);
                        }
                    }
                }

                GUILayout.EndVertical();
            }

            //GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndScrollView();
        }

        public bool GetResult(int b)
        {
            return matrixBool[b];
        }

        private void ChangeMatrixResult()
        {
            _matrixCreator.ChangeValue(selectedSlot);
        }
    }
}
