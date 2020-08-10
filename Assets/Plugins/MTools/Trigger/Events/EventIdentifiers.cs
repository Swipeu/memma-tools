using UnityEngine;
using UnityEditor;
using MTools.Identifier;

namespace MTools.Trigger.Event
{
    public static class EventIdentifiers
    {
        public static UniqueIdentifier<GameObject> TriggeringGameObject = new UniqueIdentifier<GameObject>();
        public static UniqueIdentifier<CollisionType> CollisionType = new UniqueIdentifier<CollisionType>();
    }
}