#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Logic.Script.Database;
using Logic.Script.Game;
using Logic.Script.Holder;
using UnityEditor;
using UnityEngine;

namespace Logic.Script.Window
{
    public class MatrixCreator : EditorWindow, IMatrixWindow
    {
        [SerializeField] private MatrixDatabase database;
        [SerializeField] private HolderObject rowsObject;
        [SerializeField] private HolderObject columnObject;
        [SerializeField] private float size = 60;
        [SerializeField] private string currentMouse;
        [SerializeField] private Vector2 scrollPosition;
        [SerializeField] private Vector2 horizontalScrollPosition;
        [SerializeField] public Vector2 selectedSlot1;
        [SerializeField] public Vector2 selectedSlot2;
        private BaseMatrixSlot[,] _matrix;
        private string[,] _matrixIdSlot;
        private int[,] _matrixB;
        private static DatabaseOptionWindow _databaseOptionWindow;
        private HorizontalOptionWindow _horizontalOptionWindow;
        private HorizontalConditionOptionWindow _horizontalConditionOptionWindow;
        private VerticalOptionWindow _verticalOptionWindow;
        private VerticalConditionOptionWindow _verticalConditionOptionWindow;
        private EnumMatrixCreatorMode _mode;
        private Vector2 verticalScrollPosition = Vector2.zero;


        [MenuItem("Window/MatrixCreator")]
        public static void ShowWindow()
        {
            _databaseOptionWindow = GetWindow<DatabaseOptionWindow>("DatabaseOptionWindow");
        }

        public void SetMode(EnumMatrixCreatorMode mode)
        {
            _mode = mode;
        }

        private void OnGUI()
        {
            switch (_mode)
            {
                case EnumMatrixCreatorMode.WithLocation:
                    CreateMatrix();
                    break;
                case EnumMatrixCreatorMode.WithOutLocation:
                    CreateMatrixCondition();
                    break;
            }
        }

        public void CreateMatrix()
        {
            if (GUILayout.Button("ChangeDatabase"))
            {
                Close();
                _databaseOptionWindow = GetWindow<DatabaseOptionWindow>("DatabaseOptionWindow");
            }

            database = _databaseOptionWindow.database;
            rowsObject = _databaseOptionWindow.rowsObject;
            columnObject = _databaseOptionWindow.columnObject;
            if (database != null && rowsObject != null && columnObject != null)
            {
                size = EditorGUILayout.Slider("CellSize", size, 10, 300, GUILayout.Width(150));
                currentMouse = EditorGUILayout.TextField(currentMouse);
                if (_matrixB == null || _matrixB.GetLength(0) != rowsObject.CheckCount() ||
                    _matrixB.GetLength(1) != columnObject.CheckCount())
                {
                    rowsObject.CreateSort();
                    columnObject.CreateSort();
                    _matrix = new BaseMatrixSlot[rowsObject.CheckCount(), columnObject.CheckCount()];
                    _matrixB = new int[rowsObject.CheckCount(), columnObject.CheckCount()];
                    _matrixIdSlot = new string[rowsObject.CheckCount(), columnObject.CheckCount()];
                    LoadSlots();
                }

                if (GUILayout.Button("SaveDatabase"))
                {
                    rowsObject.CreateSort();
                    columnObject.CreateSort();
                    database.objects = new List<MatrixObject>();
                    database.values = new List<MatrixValue>();
                    Save(rowsObject.CheckCount(), columnObject.CheckCount());
                    database.Save();
                    database.SetRowAndColumns(rowsObject.CheckCount(), columnObject.CheckCount());
                }

                if (GUILayout.Button("SelectAll"))
                {
                    for (int i = 0; i < rowsObject.CheckCount(); i++)
                    {
                        for (int j = 0; j < columnObject.CheckCount(); j++)
                        {
                            if (i != 0 && j != 0)
                            {
                                _matrixB[i, j] = 1;
                            }
                        }
                    }
                }

                if (GUILayout.Button("DeSelectAll"))
                {
                    for (int i = 0; i < rowsObject.CheckCount(); i++)
                    {
                        for (int j = 0; j < columnObject.CheckCount(); j++)
                        {
                            if (i != 0 && j != 0)
                            {
                                _matrixB[i, j] = 0;
                            }
                        }
                    }
                }

                float windowHeight = position.height; // ارتفاع صفحه
                windowHeight -= 200;
                float windowWidth = position.width; // ارتفاع صفحه
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(windowHeight));
                verticalScrollPosition = EditorGUILayout.BeginScrollView(verticalScrollPosition,
                    GUILayout.Width(windowWidth), GUILayout.Height(windowHeight));

                DrawMatrix();
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndScrollView();

                //DrawMoveSlotsButtons();
            }
        }


        private void DrawMatrix()
        {
            if (database != null)
            {
                Rect matrixRect = EditorGUILayout.GetControlRect(false, rowsObject.CheckCount() * size,
                    GUILayout.Width(columnObject.CheckCount() * size));

                for (int i = 0; i < rowsObject.CheckCount(); i++)
                {
                    GUILayout.BeginHorizontal();

                    for (int j = 0; j < columnObject.CheckCount(); j++)
                    {
                        Rect cellRect = new Rect(j * size, i * size, size, size);
                        EditorGUI.DrawRect(cellRect, Color.white);

                        if (selectedSlot1 == new Vector2(i, j))
                        {
                            GUI.color = Color.green;
                        }

                        if (selectedSlot2 == new Vector2(i, j))
                        {
                            GUI.color = Color.red;
                        }

                        if (i == 0 || j == 0)
                        {
                            if (i == 0)
                            {
                                _matrix[i, j] = columnObject.ConvertToMatrixSlot(j);
                                var button = GUI.Button(cellRect, _matrix[i, j].Tag);
                                _matrixIdSlot[i, j] = button.ToString();
                                if (Event.current.type == EventType.Repaint)
                                {
                                    var buttonRect = cellRect;

                                    if (buttonRect.Contains(Event.current.mousePosition))
                                    {
                                        currentMouse = _matrix[i, j].Tag;
                                    }
                                }

                                if (button)
                                {
                                    _horizontalOptionWindow = GetWindow<HorizontalOptionWindow>(_matrix[i, j].Tag);
                                    _horizontalOptionWindow.SetMatrixSlot(_matrix[i, j], columnObject, this, _matrixB,
                                        rowsObject);
                                }
                            }

                            if (j == 0)
                            {
                                _matrix[i, j] = rowsObject.ConvertToMatrixSlot(i);
                                var button = GUI.Button(cellRect, _matrix[i, j].Tag);
                                _matrixIdSlot[i, j] = button.ToString();
                                if (Event.current.type == EventType.Repaint)
                                {
                                    var buttonRect = cellRect;

                                    if (buttonRect.Contains(Event.current.mousePosition))
                                    {
                                        currentMouse = _matrix[i, j].Tag;
                                    }
                                }

                                if (button)
                                {
                                    _verticalOptionWindow = GetWindow<VerticalOptionWindow>(_matrix[i, j].Tag);
                                    _verticalOptionWindow.SetMatrixSlot(_matrix[i, j], columnObject, this, _matrixB,
                                        rowsObject);
                                }
                            }

                        }
                        else
                        {
                            _matrixB[i, j] = EditorGUI.IntField(cellRect, _matrixB[i, j]);
                        }

                        GUI.color = Color.white;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        public void CreateMatrixCondition()
        {
            if (GUILayout.Button("ChangeDatabase"))
            {
                Close();
                _databaseOptionWindow = GetWindow<DatabaseOptionWindow>("DatabaseOptionWindow");
            }

            database = _databaseOptionWindow.database;
            rowsObject = _databaseOptionWindow.rowsObject;
            columnObject = _databaseOptionWindow.columnObject;

            if (database != null && rowsObject != null && columnObject != null)
            {
                size = EditorGUILayout.Slider("CellSize", size, 10, 300, GUILayout.Width(150));
                currentMouse = EditorGUILayout.TextField(currentMouse);

                if (_matrixB == null || _matrixB.GetLength(0) != rowsObject.CheckCountWithoutChild() ||
                    _matrixB.GetLength(1) != columnObject.CheckCountWithoutChild())
                {
                    _matrix = new BaseMatrixSlot[rowsObject.CheckCountWithoutChild(),
                        columnObject.CheckCountWithoutChild()];
                    _matrixB = new int[rowsObject.CheckCountWithoutChild(), columnObject.CheckCountWithoutChild()];
                    _matrixIdSlot = new string[rowsObject.CheckCountWithoutChild(),
                        columnObject.CheckCountWithoutChild()];
                    LoadSlotConditions();
                }

                if (GUILayout.Button("SaveDatabase"))
                {
                    database.objects = new List<MatrixObject>();
                    database.values = new List<MatrixValue>();
                    Save(rowsObject.CheckCountWithoutChild() , columnObject.CheckCountWithoutChild());
                    database.Save();
                    database.SetRowAndColumns(rowsObject.CheckCountWithoutChild(),
                        columnObject.CheckCountWithoutChild());
                }

                if (GUILayout.Button("SelectAll"))
                {
                    for (int i = 0; i < rowsObject.CheckCountWithoutChild(); i++)
                    {
                        for (int j = 0; j < columnObject.CheckCountWithoutChild(); j++)
                        {
                            if (i != 0 && j != 0)
                            {
                                _matrixB[i, j] = 1;
                            }
                        }
                    }
                }

                if (GUILayout.Button("DeSelectAll"))
                {
                    rowsObject.CreateSort();
                    columnObject.CreateSort();
                    for (int i = 0; i < rowsObject.CheckCountWithoutChild(); i++)
                    {
                        for (int j = 0; j < columnObject.CheckCountWithoutChild(); j++)
                        {
                            if (i != 0 && j != 0)
                            {
                                _matrixB[i, j] = 0;
                            }
                        }
                    }
                }

                float windowHeight = position.height; // ارتفاع صفحه
                windowHeight -= 200;
                float windowWidth = position.width; // ارتفاع صفحه
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(windowHeight));
                verticalScrollPosition = EditorGUILayout.BeginScrollView(verticalScrollPosition,
                    GUILayout.Width(windowWidth), GUILayout.Height(windowHeight));
                DrawMatrixCondition();
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndScrollView();

                //DrawMoveSlotsButtons();
            }
        }

        private void DrawMatrixCondition()
        {
            if (database != null)
            {
                Rect matrixRect = EditorGUILayout.GetControlRect(false, rowsObject.CheckCountWithoutChild() * size,
                    GUILayout.Width(columnObject.CheckCountWithoutChild() * size));
                GUI.Box(matrixRect, "");


                for (int i = 0; i < rowsObject.CheckCountWithoutChild(); i++)
                {
                    GUILayout.BeginHorizontal();

                    for (int j = 0; j < columnObject.CheckCountWithoutChild(); j++)
                    {
                        Rect cellRect = new Rect(j * size, i * size, size, size);
                        EditorGUI.DrawRect(cellRect, Color.white);

                        if (selectedSlot1 == new Vector2(i, j))
                        {
                            GUI.color = Color.green;
                        }

                        if (selectedSlot2 == new Vector2(i, j))
                        {
                            GUI.color = Color.red;
                        }

                        if (i == 0 || j == 0)
                        {
                            if (i == 0)
                            {
                                _matrix[i, j] = columnObject.ConvertToSlot(j);
                                var button = GUI.Button(cellRect, _matrix[i, j].Tag);
                                _matrixIdSlot[i, j] = button.ToString();
                                if (Event.current.type == EventType.Repaint)
                                {
                                    var buttonRect = cellRect;

                                    if (buttonRect.Contains(Event.current.mousePosition))
                                    {
                                        currentMouse = _matrix[i, j].Tag;
                                    }
                                }

                                if (button)
                                {
                                    _horizontalConditionOptionWindow =
                                        GetWindow<HorizontalConditionOptionWindow>(_matrix[i, j].Tag);
                                    _horizontalConditionOptionWindow.SetMatrixSlot(_matrix[i, j], columnObject, this,
                                        _matrixB,
                                        rowsObject);
                                }
                            }
                            else if (j == 0)
                            {
                                _matrix[i, j] = rowsObject.ConvertToSlot(i);
                                var button = GUI.Button(cellRect, _matrix[i, j].Tag);
                                _matrixIdSlot[i, j] = button.ToString();
                                if (Event.current.type == EventType.Repaint)
                                {
                                    var buttonRect = cellRect;

                                    if (buttonRect.Contains(Event.current.mousePosition))
                                    {
                                        currentMouse = _matrix[i, j].Tag;
                                    }
                                }

                                if (button)
                                {
                                    _verticalConditionOptionWindow =
                                        GetWindow<VerticalConditionOptionWindow>(_matrix[i, j].Tag);
                                    _verticalConditionOptionWindow.SetMatrixSlot(_matrix[i, j], columnObject, this,
                                        _matrixB,
                                        rowsObject);
                                }
                            }
                        }
                        else
                        {
                            _matrixB[i, j] = EditorGUI.IntField(cellRect, _matrixB[i, j]);
                        }


                        GUI.color = Color.white;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        private void Save(int rows, int column)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        SaveObject(new Vector2(i, j), _matrix[i, j]);
                    }
                    else
                    {
                        SaveValue(new Vector2(i, j), _matrixB[i, j]);
                    }
                }
            }
        }

        private void SaveValue(Vector2 vector2, int i)
        {
            database.SaveValue(vector2, i);
        }

        private void DrawMoveSlotsButtons()
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Select Slot 1:");
            selectedSlot1 = EditorGUILayout.Vector2Field("", selectedSlot1);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Select Slot 2:");
            selectedSlot2 = EditorGUILayout.Vector2Field("", selectedSlot2);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Move Slots"))
            {
                MoveSlots(selectedSlot1, selectedSlot2);
            }

            GUILayout.Space(20);
        }

        private void MoveSlots(Vector2 slot1, Vector2 slot2)
        {
            if (IsValidSlot(slot1) && IsValidSlot(slot2))
            {
                (_matrix[(int)slot1.x, (int)slot1.y], _matrix[(int)slot2.x, (int)slot2.y]) = (
                    _matrix[(int)slot2.x, (int)slot2.y], _matrix[(int)slot1.x, (int)slot1.y]);
                SaveObject(slot1, _matrix[(int)slot1.x, (int)slot1.y]);
                SaveObject(slot2, _matrix[(int)slot2.x, (int)slot2.y]);
                selectedSlot1 = Vector2.zero;
                selectedSlot2 = Vector2.zero;
            }
            else
            {
                Debug.LogError("Invalid slot selection. Make sure both slots are within the matrix range.");
            }
        }

        private bool IsValidSlot(Vector2 slot)
        {
            return slot.x >= 0 && slot.x < rowsObject.CheckCount() && slot.y >= 0 &&
                   slot.y < columnObject.CheckCount();
        }

        private void SaveObject(Vector2 cor, BaseMatrixSlot obj)
        {
            database.SaveObject(cor, obj);
        }

        private void LoadSlots()
        {
            for (int i = 0; i < rowsObject.CheckCount(); i++)
            {
                for (int j = 0; j < columnObject.CheckCount(); j++)
                {
                    Vector2 key = new Vector2(i, j);
                    if (i == 0 || j == 0)
                    {
                        _matrix[i, j] = database.LoadObject(key);
                    }
                    else
                    {
                        _matrixB[i, j] = database.LoadValue(key);
                    }
                }
            }
        }

        private void LoadSlotConditions()
        {
            for (int i = 0; i < rowsObject.Objects.Length; i++)
            {
                for (int j = 0; j < columnObject.Objects.Length; j++)
                {
                    Vector2 key = new Vector2(i, j);
                    if (i == 0 || j == 0)
                    {
                        _matrix[i, j] = database.LoadObject(key);
                    }
                    else
                    {
                        _matrixB[i, j] = database.LoadValue(key);
                    }
                }
            }
        }

        public void ChangeValue(BaseMatrixSlot slot)
        {
            var vec = database.GetSlot(slot);
            for (int i = 0; i < rowsObject.CheckCount(); i++)
            {
                for (int j = 0; j < columnObject.CheckCount(); j++)
                {
                    if (j == vec.y)
                    {
                        if (i != 0)
                        {
                            Rect cellRect = new Rect(j * size, i * size, size, size);
                            _matrixB[i, j] = EditorGUI.IntField(cellRect,
                                Convert.ToInt32(_horizontalOptionWindow.GetResult(i)));
                            SaveValue(new Vector2(i, j), _matrixB[i, j]);
                        }
                    }
                }
            }
        }

        public void ChangeValueCondition(BaseMatrixSlot slot)
        {
            var vec = database.GetSlot(slot);
            for (int i = 0; i < rowsObject.Objects.Length; i++)
            {
                for (int j = 0; j < columnObject.Objects.Length; j++)
                {
                    if (j == vec.y)
                    {
                        if (i != 0)
                        {
                            Rect cellRect = new Rect(j * size, i * size, size, size);
                            _matrixB[i, j] = EditorGUI.IntField(cellRect,
                                _horizontalConditionOptionWindow.GetResult(i));
                            SaveValue(new Vector2(i, j), _matrixB[i, j]);
                        }
                    }
                }
            }
        }

        public void ChangeValueVertical(BaseMatrixSlot slot)
        {
            var vec = database.GetSlot(slot);
            for (int i = 0; i < rowsObject.CheckCount(); i++)
            {
                for (int j = 0; j < columnObject.CheckCount(); j++)
                {
                    if (i == vec.y)
                    {
                        if (j != 0)
                        {
                            Rect cellRect = new Rect(j * size, i * size, size, size);
                            _matrixB[i, j] = EditorGUI.IntField(cellRect,
                                Convert.ToInt32(_verticalOptionWindow.GetResult(j)));
                            SaveValue(new Vector2(i, j), _matrixB[i, j]);
                        }
                    }
                }
            }
        }

        public void ChangeValueVerticalCondition(BaseMatrixSlot slot)
        {
            var vec = database.GetSlot(slot);
            for (int i = 0; i < rowsObject.Objects.Length; i++)
            {
                for (int j = 0; j < columnObject.Objects.Length; j++)
                {
                    if (i == vec.x)
                    {
                        if (j != 0)
                        {
                            Rect cellRect = new Rect(j * size, i * size, size, size);
                            _matrixB[i, j] = EditorGUI.IntField(cellRect,
                                _verticalConditionOptionWindow.GetResult(j));
                            SaveValue(new Vector2(i, j), _matrixB[i, j]);
                        }
                    }
                }
            }
        }

        public Vector2 GetLocation(BaseMatrixSlot slot)
        {
            return database.GetSlot(slot);
        }
    }
}

#endif