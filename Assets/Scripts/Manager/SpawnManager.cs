using UnityEngine;

namespace Manager
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject Spawn(GameObject obj,Transform parent)
        {
           return Instantiate(obj, parent);
        }
        public GameObject Spawn(GameObject obj,Vector3 position , Quaternion rotation)
        {
           return Instantiate(obj, position , rotation);
        }
    }
}