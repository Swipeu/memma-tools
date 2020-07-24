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
