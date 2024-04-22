using Logic.Script.Holder;
using Logic.Script.Window;
using UnityEditor;

namespace Logic.Script.Database
{
    public class DatabaseOptionWindow : EditorWindow
    {
        public MatrixDatabase database;
        public HolderObject rowsObject;
        public HolderObject columnObject;
        private int _row;
        private int _column;

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 80f;
            database =
                EditorGUILayout.ObjectField("Database", database, typeof(MatrixDatabase), true) as
                    MatrixDatabase;
            if (database != null)
            {
                columnObject = database.columnObject;
                rowsObject = database.rowsObject;
                switch (database.mode)
                {
                    case EnumMatrixCreatorMode.WithLocation:
                        GetWindow<MatrixCreator>(database.id).SetMode(EnumMatrixCreatorMode.WithLocation);
                        Close();
                        break;
                    case EnumMatrixCreatorMode.WithOutLocation:
                        GetWindow<MatrixCreator>(database.id).SetMode(EnumMatrixCreatorMode.WithOutLocation);
                        Close();
                        break;
                }
            }
        }
    }
}