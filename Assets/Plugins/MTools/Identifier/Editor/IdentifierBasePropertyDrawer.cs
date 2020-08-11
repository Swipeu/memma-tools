using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MTools.Identifier
{
    [CustomPropertyDrawer(typeof(IdentifierBase), true)]
    public class IdentifierBasePropertyDrawer : PropertyDrawer
    {
        static List<Type> identifierRoots = new List<Type>();
        static bool fetchedIdentifierRoots = false;

        static List<Type> IdentifierRoots
        {
            get
            {
                if (!fetchedIdentifierRoots)
                {
                    var foundIdentifierRoots = from a in AppDomain.CurrentDomain.GetAssemblies()
                                               from t in a.GetTypes()
                                               let attributes = t.GetCustomAttributes(typeof(IdentifierRoot), true)
                                               where attributes != null && attributes.Length > 0
                                               select t;

                    identifierRoots = foundIdentifierRoots.ToList();
                    fetchedIdentifierRoots = true;
                }

                return identifierRoots;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var path = property.FindPropertyRelative("path");

            List<string> options = GetIdentifierPropertyPaths(property.type);

            int index = 0;
            if (!string.IsNullOrEmpty(path.stringValue))
            {
                if (options.Contains(path.stringValue))
                {
                    index = options.IndexOf(path.stringValue);
                }
                else
                {
                    index = options.Count;
                    options.Add(path.stringValue);
                }
            }

            var style = new GUIStyle(EditorStyles.popup);
            style.alignment = TextAnchor.MiddleRight;


            List<string> visualList = new List<string>();

            foreach (string option in options)
            {
                string[] optionParts = option.Split('+', '.');
                string test = string.Join(".", optionParts, 0, optionParts.Length - 1);
                test += $"/{optionParts[optionParts.Length - 1]}";
                visualList.Add(test);
            }

            int newIndex = EditorGUI.Popup(position, property.displayName, index, visualList.ToArray(), style);

            if (index != newIndex)
                property.FindPropertyRelative("needRefresh").boolValue = true;

            path.stringValue = options[newIndex];
            property.FindPropertyRelative("checkInitiatedFromConstructor").boolValue = true;
        }
        private List<string> GetIdentifierPropertyPaths(string propertyTypeName)
        {
            List<string> paths = new List<string>();

            foreach (var type in IdentifierRoots)
            {
                paths.AddRange(GetIdentifierPropertiesInType(type, propertyTypeName, $"{type.Namespace}.{type.Name}"));
            }

            return paths;
        }
        private List<string> GetIdentifierPropertiesInType(Type type, string propertyTypeName, string path)
        {
            List<string> paths = new List<string>();

            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(p => p.PropertyType.Name == propertyTypeName);

            foreach (PropertyInfo property in properties)
            {
                paths.Add($"{path}.{property.Name}");
            }

            foreach (var nestedType in type.GetNestedTypes())
            {
                paths.AddRange(GetIdentifierPropertiesInType(nestedType, propertyTypeName, $"{path}+{nestedType.Name}"));
            }

            return paths;
        }

    }
}
