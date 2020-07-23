using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MTools.Reorderable
{
    [CustomPropertyDrawer(typeof(ReorderableList<>))]
    public class ReorderableListPropertyDrawer : ReorderablePropertyDrawer
    {
        protected override float GetElementHeight(SerializedProperty element)
        {
            return EditorGUI.GetPropertyHeight(element, true) + padding * 2;
        }

        protected override void DrawElement(SerializedProperty element, Rect rect, bool isActive, bool isFocused)
        {
            rect.position += Vector2.up * padding;
            rect.height += padding;

            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(rect, element, GUIContent.none, true);
            EditorGUI.indentLevel--;
        }
    }
}