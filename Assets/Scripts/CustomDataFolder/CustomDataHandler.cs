using System;
using System.Collections.Generic;
using CustomDataFolder;
using Script.Ext;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.CustomDataFolder
{
    public class CustomDataHandler : MonoBehaviour
    {
        public List<CustomData> data;

        public void SetValue(string key, object value)
        {
          data.TrySetOrAdd(key,value);
        }
        public T GetValue<T>(string key)
        { 
            var index = data.FindIndex(i => i.key == key);
            if(index != -1)
            {
                return (T)data[index].value;
            }
            Debug.Log(key + " not found");
            return default;
            
        }
        public List<CustomData> GetAllValue(string key)
        { 
            var find = data.FindAll(i => i.key == key);
            return find;
        }
    }
}