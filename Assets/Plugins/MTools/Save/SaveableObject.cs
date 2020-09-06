using UnityEngine;
using System.Linq;
using MTools.Attributes;

namespace MTools.Save
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode] 
    public class SaveableObject : MonoBehaviour
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {
        [SerializeField, ReadOnly] string guid;

        void ApplyGUID()
        {
            if (!string.IsNullOrEmpty(guid))
                return;

            guid = System.Guid.NewGuid().ToString();
        }

        void ClearGUID()
        {
            guid = string.Empty;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(guid))
                return;

            var components = GetComponents<MonoBehaviour>();

            foreach(var component in components)
            {
                if (component == this)
                    continue;

                var propertyFields = component.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(SavedAttribute), true));

                foreach(var propertyField in propertyFields)
                {
                    //propertyField.GetValue();
                }
            }
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            Event e = Event.current;

            if (e != null && e.type == EventType.ExecuteCommand && (e.commandName == "Duplicate" || e.commandName == "Paste"))
                ClearGUID();
        }

        public void OnBeforeSerialize()
        {
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject))
            {
                ClearGUID();
                return;
            }

            var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();

            if (prefabStage != null && prefabStage.IsPartOfPrefabContents(gameObject))
            {
                ClearGUID();
                return;
            }

            ApplyGUID();
        }

        public void OnAfterDeserialize()
        {

        }
#endif
    }
}

