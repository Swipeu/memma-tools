# memma-tools
Collection of custom Unity tools

## Reorderable
Requires Unity 2020.1 or later.
Created with `UnityEditorInternal.ReorderableList`.

#### List
`ReorderableList<Type>`
- Inherits directly from `System.Collections.Generic.List<Type>`.
- Can handle any serializable type.

#### Dictionary
`ReorderableDictionary<Key, Value>`
- Inherits directly from `System.Collections.Generic.Dictionary<Key, Value>`.
- Can handle any serializable type as key and value.
- Marks conflicting keys in inspector and removes them from the dictionary until fixed.

## Identifier
Create serializable identifiers that can be accessed by code or the unity inspector

#### String
`StringIdentifier` or `StringIdentifier<Type>`
-Using a defined string and type for comparison

#### Unique
`UniqueIdentifier` or `UniqueIdentifier<Type>`
- Generates a new identifier when creating instances
- Using a generated identifier and type for comparison

## Trigger
Simple trigger system

#### TriggerSource
`TriggerSource<EventType>`
- Contains serialized list of TriggerTargets to notify on trigger

#### TriggerTarget
`TriggerTarget` and `ITriggerTarget<T>`

#### TriggerEvent
`ITriggerEvent`
