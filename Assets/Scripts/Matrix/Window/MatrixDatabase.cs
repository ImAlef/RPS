using System.Collections.Generic;
using Logic.Script.Game;
using Logic.Script.Holder;
using UnityEditor;
using UnityEngine;

namespace Logic.Script.Window
{
    [CreateAssetMenu(menuName = "Matrix/Database", fileName = "Database")]
    public class MatrixDatabase : ScriptableObject
    {
        public string id;
        public HolderObject rowsObject;
        public HolderObject columnObject;
        public EnumMatrixCreatorMode mode;
        int[] _rc = new int[3];
        public List<MatrixValue> values;
        public List<MatrixObject> objects;

        public void SetRowAndColumns(int r, int c)
        {
            _rc[0] = r;
            _rc[1] = c;
        }

        public int[] GetRowAndColumns()
        {
            return _rc;
        }

        public void FindObject()
        {
        }

        public void SaveValue(Vector2 coordinates, int value)
        {
            var f = values.Find(i => i.Key == coordinates);
            if (f != null)
            {
                int i = values.FindIndex(i => i.Key == f.Key);
                values[i] = new MatrixValue(coordinates, value);
            }
            else values.Add(new MatrixValue(coordinates, value));
        }

        public void SaveObject(Vector2 coordinates, BaseMatrixSlot obj)
        {
            var f = objects.Find(i => i.Key == coordinates);
            if (f != null)
            {
                int i = objects.FindIndex(i => i.Key == f.Key);
                objects[i] = new MatrixObject(coordinates, obj);
            }
            else objects.Add(new MatrixObject(coordinates, obj));
        }

        public int LoadValue(Vector2 coordinates)
        {
            var o = values.Find(i => i.Key == coordinates);
            if (o == null) return 0;
            return o.Result;
        }

        public BaseMatrixSlot LoadObject(Vector2 coordinates)
        {
            var o = objects.Find(i => i.Key == coordinates);
            if (o == null) return null;
            return o.Ability;
        }

        public void DeleteKey(int i)
        {
            values.RemoveRange(i, values.Count - i);
            objects.RemoveRange(i, objects.Count - i);
        }

        public void Reset()
        {
            values = new List<MatrixValue>();
        }

        public int GetResult(BaseMatrixSlot vertical, BaseMatrixSlot horizontal)
        {
            MatrixObject finderA = new MatrixObject(new Vector2(), new BaseMatrixSlot(""));
            MatrixObject finderB = new MatrixObject(new Vector2(), new BaseMatrixSlot(""));
            foreach (var o in objects)
            {
                if (o.key.y == 0)
                {
                    if (o.Ability.Tag == vertical.Tag)
                    {
                        finderA = o;
                    }
                }

                if (o.Key.x == 0)
                {
                    if (o.Ability.tag == horizontal.Tag)
                    {
                        finderB = o;
                    }
                }
            }


            var coordinates = new Vector2(finderA.key.x, finderB.key.y);
            var resultObject = values.Find(i => i.key == coordinates);
            if (resultObject == null)
            {
                Debug.LogError($"Null Reference : {vertical.tag} && {horizontal.Tag}");
                return 0;
            }
            return resultObject.result;
        }

        public Dictionary<string, int> GetVerticalResult(BaseMatrixSlot vertical)
        {
            var stringList = new List<string>();
            var intList = new List<int>();
            Dictionary<string, int> result = new Dictionary<string, int>();
            var v = objects.Find(i => i.ability.tag == vertical.tag);
            foreach (var o in objects)
            {
                if (o.key.y == 0) //vertical
                {
                    stringList.Add(o.ability.tag);
                }
            }

            intList.Add(0);
            foreach (var value in values)
            {
                if (value.key.y == v.key.y)
                {
                    intList.Add(value.result);
                }
            }

            for (int i = 0; i < intList.Count; i++)
            {
                result.Add(stringList[i], intList[i]);
            }

            foreach (var res in result)
            {
            }

            return result;
        }


        public Vector2 GetSlot(BaseMatrixSlot slot)
        {
            var matrixObject = objects.Find(i => i.Ability.Tag == slot.Tag);
            if (matrixObject != null)
            {
                return matrixObject.Key;
            }

            return new Vector2();
        }

        public void Save()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}