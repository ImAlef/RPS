using System;
using Manager;
using Script.Ext;
using Sirenix.Serialization;
using UnityEngine;

namespace Variable
{
    public class GetGameObjectOfListCustomDataWithString
    {
        public GameManager GameManager;
        public string id;
        
        public GameObject GetValue()
        {
            var customData = GameManager.CustomData;
            var key = customData.GetValue<string>(id);
            return customData.GetValue<GameObject>(key);
        }
    }
}