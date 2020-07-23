using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;

namespace MTools.Reorderable
{
    public abstract class ReorderablePropertyDrawer : PropertyDrawer
    {
        protected const int padding = 5;

        private Dictionary<string, ReorderableList> lists = new Dictionary<string, ReorderableList>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var reorderableList = GetOrCreateList(property);
            reorderableList.DoList(position);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetOrCreateList(property).GetHeight();
        }

        ReorderableList GetOrCreateList(SerializedProperty serializedProperty)
        {
            if (!lists.TryGetValue(serializedProperty.propertyPath, out ReorderableList list) || list.draggable != serializedProperty.isExpanded)
            {
                var serializedList = serializedProperty.FindPropertyRelative("serializedList");

                // Draw empty list if collapsed
                if (!serializedProperty.isExpanded)
                {
                    list = new ReorderableList(new List<object>(), typeof(object), false, true, false, false);
                }
                else
                {
                    list = new ReorderableList(serializedProperty.serializedObject, serializedList, true, true, true, true);
                }

                list.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.indentLevel++;
                    serializedProperty.isExpanded = EditorGUI.Foldout(rect, serializedProperty.isExpanded, serializedProperty.displayName);
                    EditorGUI.indentLevel--;
                };

                list.elementHeightCallback = (int index) =>
                {
                    return GetElementHeight(serializedList.GetArrayElementAtIndex(index));
                };

                list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    DrawElement(serializedList.GetArrayElementAtIndex(index), rect, isActive, isFocused);
                };

                lists[serializedProperty.propertyPath] = list;
            }

            list.drawNoneElementCallback = (Rect rect) =>
            {
                if (serializedProperty.isExpanded)
                {
                    EditorGUI.LabelField(rect, "Empty");
                }
                else
                {
                    EditorGUI.LabelField(rect, "Collapsed");
                }
            };

            return list;
        }

        protected abstract float GetElementHeight(SerializedProperty element);
        protected abstract void DrawElement(SerializedProperty element, Rect rect, bool isActive, bool isFocused);
    }
}
