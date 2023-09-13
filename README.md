# Unity-Remember-Self-Prefab

In Unity, GameObjects derived from prefabs don't inherently keep a reference to those prefabs. Furthermore, to prevent serialization issues, when a prefab with a reference to itself is instantiated, the new object gets a reference to itself -- not the original prefab.

This repository provides a few scripts that allow developers to easily create and maintain stable references to source prefabs.

## How to use:

Copy **RememberPrefab.cs** to your scripts folder and **RememberPrefabCustomEditor.cs** to a folder named Editor.

Add **RememberPrefab** as a component to the desired object and select the appropriate prefab reference in the inspector. Note: Due to limitations on how Unity loads files in runtime, RememberPrefab can only store references to **prefabs inside of a folder named Resources**. This folder can be anywhere, there can be multiple such folders, and it can have its own subfolders. But there needs to be a folder named "Resources" somewhere in the path for this component to work. See https://docs.unity3d.com/ScriptReference/Resources.html for more information.

![image](https://github.com/skylord-a52/Unity-Remember-Self-Prefab/assets/12107211/38f65f57-bcbc-4021-bcbf-9d1a701fbf07)

Call `GetComponent<RememberPrefab>().Prefab` to get a reference to the prefab in code -- this works in either Edit or Play mode, and the reference always points to the same object no matter what.


## Notes

Not guaranteed to work correctly when values of RememberPrefab are modified via script instead of the inspector, particularly during builds.