
namespace Logic.Script.Game
{
   
    [System.Serializable]
    public class BaseMatrixSlot
    {
        public string tag;

        public BaseMatrixSlot(string tag)
        {
            this.tag = tag;
        }

        public  string Tag { get => tag; set => tag = value; }
    }
}