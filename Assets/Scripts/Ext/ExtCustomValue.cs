using System.Collections.Generic;
using CustomDataFolder;
using Script.Action.Arg;
using Script.CustomDataFolder;
using UnityEngine;

namespace Script.Ext
{
    public static class ExtCustomValue
    {
        public static void TrySetOrAdd(this List<CustomData> data ,string key, object value)
        {
            var index = data.FindIndex(i => i.key == key);
            if(index == -1)data.Add(new CustomData(key , value));
            else
            {
                data[index] = new CustomData(key, value);
            }
        }

        public static T GetValue<T>(this List<CustomData> data ,string key)
        {
            var index = data.FindIndex(i => i.key == key);
            if(index != -1)
            {
                return (T)data[index].value;
            }
            Debug.Log(key + " not found");
            return default;
            
        }

        public static void TryRemove(this List<CustomData> list,string key)
        {
            var index = list.FindIndex(i => i.key == key);
            if (index != -1) list.Remove(list[index]);
        }
    }
}