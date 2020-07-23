using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MTools.Reorderable
{
    [CustomPropertyDrawer(typeof(ReorderableDictionary<,>))]
    public class ReorderableDictionaryPropertyDrawer : ReorderablePropertyDrawer
    {
        protected override float GetElementHeight(SerializedProperty element)
        {
            var key = element.FindPropertyRelative("key");
            var value = element.FindPropertyRelative("value");

            return EditorGUI.GetPropertyHeight(key, true) + EditorGUI.GetPropertyHeight(value, true) + padding * 2;
        }

        protected override void DrawElement(SerializedProperty element, Rect rect, bool isActive, bool isFocused)
        {
            var key = element.FindPropertyRelative("key");
            var value = element.FindPropertyRelative("value");
            var conflicting = element.FindPropertyRelative("conflicting");

            rect.position += Vector2.up * padding;
            rect.height += padding;

            if (conflicting.boolValue)
            {
                var guiContent = EditorGUIUtility.IconContent("CollabConflict");
                guiContent.tooltip = "This key is conflicting with\nanother key in the dictionary";
                EditorGUI.LabelField(rect, guiContent, new GUIStyle()
                {
                    alignment = TextAnchor.UpperLeft,
                    padding = new RectOffset(0, 0, (int)EditorGUIUtility.singleLineHeight / 2 - guiContent.image.height / 2, 0)
                });
            }

            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(rect, key, true);
            rect.position += Vector2.up * EditorGUI.GetPropertyHeight(key, true);
            EditorGUI.PropertyField(rect, value, true);
            EditorGUI.indentLevel--;
        }
    }
}