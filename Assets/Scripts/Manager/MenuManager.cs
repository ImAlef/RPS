using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class MenuManager : MonoBehaviour
    {
        public void LoadScene(string id)
        {
            SceneManager.LoadScene(id);
        }
    }
}