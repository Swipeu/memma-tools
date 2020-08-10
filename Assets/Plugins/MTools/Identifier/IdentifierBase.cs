using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;

namespace MTools.Identifier
{
    [Serializable]
    public abstract class IdentifierBase
    {
        private string identifier = "";
        private bool initiatedFromConstructor = true;
        [SerializeField] private string path = "";
        [SerializeField] private bool needRefresh = false;
        [SerializeField] private bool checkInitiatedFromConstructor = false;

        protected string Identifier
        {
            get
            {
                RefreshIdentifier();
                return identifier ?? "";
            }
            set
            {
                identifier = value;
            }
        }

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != this.GetType())
                return false;
                    
            return Identifier == ((IdentifierBase)obj).Identifier;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override string ToString()
        {
            return Identifier;
        }

        private void RefreshIdentifier()
        {
            if (checkInitiatedFromConstructor && initiatedFromConstructor)
                needRefresh = true;

            if (!needRefresh && 
                (!string.IsNullOrEmpty(identifier)
                || string.IsNullOrEmpty(path)))
                return;

            string propertyString = path.Split('.').Last();
            string typePath = path.Substring(0, path.Length - propertyString.Length).Trim('.');

            IdentifierBase matchingIdentifier = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(typePath, false);

                if (type == null)
                    continue;

                var propertyInfo = type.GetProperty(propertyString);

                if (propertyInfo == null || !typeof(IdentifierBase).IsAssignableFrom(propertyInfo.PropertyType))
                    continue;

                object value = (IdentifierBase)propertyInfo.GetValue(null);

                if (!(value is IdentifierBase identifierValue))
                    continue;

                matchingIdentifier = identifierValue;

                if (matchingIdentifier != null)
                    break;
            }

            if (matchingIdentifier == null || string.IsNullOrEmpty(matchingIdentifier.identifier))
                return;

            this.identifier = matchingIdentifier.identifier;
            this.needRefresh = false;
            this.initiatedFromConstructor = false;
        }
    }
}