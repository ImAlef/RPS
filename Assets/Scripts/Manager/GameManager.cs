using System.Collections.Generic;
using CustomDataFolder;
using Script.Action.Action;
using Script.Ext;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Variable;

namespace Manager
{
    [ShowOdinSerializedPropertiesInInspector]
    public class GameManager : MonoBehaviour, ISerializationCallbackReceiver , ISupportsPrefabSerialization
    {
        public SerializationData SerializationData { get => serializationData; set => serializationData =value; }
        [SerializeField, HideInInspector]
        private SerializationData serializationData;
        
        [OdinSerialize]public List<CustomData> CustomData;
        [OdinSerialize]public List<BaseAction> Actions;

        public delegate void OnDelegateChangeScore(int playerScore , int aiScore);

        public event OnDelegateChangeScore OnChangeScore;
        
        public void SetValueStatic(string value)
        {
            CustomData.TrySetOrAdd("PlayerChoose" , value);
        }

        public void OnChangeScoreEvent()
        {
            var ai = CustomData.GetValue<IntClass>("AiScore").Value;
            var player = CustomData.GetValue<IntClass>("PlayerScore").Value;
            OnOnChangeScore(player,ai);
        }
        public void RunAction(string id)
        {
            var find = FindAction(id);
            if (find != null)
            {
                find.Run(CustomData);
                return;
            }
            Debug.LogError($"{id} Action not Founded!");
        }

        private BaseAction FindAction(string key)
        {
            return Actions.Find(i => i.id == key);
        }

        public void DestroyFunction(Object obj)
        {
            Destroy(obj);
        }
        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
        }

        protected virtual void OnOnChangeScore(int playerScore , int aiScore)
        {
            OnChangeScore?.Invoke(playerScore,aiScore);
        }
    }
}