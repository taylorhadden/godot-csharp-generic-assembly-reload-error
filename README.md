# Generic Script Loading Conflict
_Built with Godot 4.1 Mono_

This is a simple test project that demonstrates an editor issue with generic C# scripts.

`GenericScript.cs` contains a simple generic script extending `Node3D`. `TestScript.cs` and `OtherTestScript.cs` both extend `GenericScript<float>`.

## Behavior Reproduction
This behavior was found on a system with the following specs:
- Godot 4.1 Mono
- `dotnet` version 6.0.401
- Windows 11 Home, version 22H22, build 22621.1848

### Step 1
With `ParentScene.tscn` loaded in the editor, going to the "MSBuild" tab and clicking on "Rebuild Project" will cause the following error to appear in the console:

```
modules/mono/glue/runtime_interop.cpp:1324 - System.ArgumentException: An item with the same key has already been added. Key: GenericScript`1[System.Single]
     at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)
     at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)
     at Godot.Bridge.ScriptManagerBridge.ScriptTypeBiMap.Add(IntPtr scriptPtr, Type scriptType) in /root/godot/modules/mono/glue/GodotSharp/GodotSharp/Core/Bridge/ScriptManagerBridge.types.cs:line 23
     at Godot.Bridge.ScriptManagerBridge.TryReloadRegisteredScriptWithClass(IntPtr scriptPtr) in /root/godot/modules/mono/glue/GodotSharp/GodotSharp/Core/Bridge/ScriptManagerBridge.cs:line 579
```

### Step 2
If "Rebuild Project" is run again at this point, you will get the following error message:

```
modules/mono/mono_gd/gd_mono.cpp:513 - .NET: Failed to unload assemblies. Please check https://github.com/godotengine/godot/issues/78513 for more information. (User)
```

At this point, Godot must be restarted. With the assembly in a bad state, data is easily lost.

### Step 3
After reloading Godot and repeating Step 1, if you now instead close "ParentScene" so that no scene is loaded, assemblies reload correctly.
