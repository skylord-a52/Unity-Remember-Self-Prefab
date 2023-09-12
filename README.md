# Unity-Remember-Self-Prefab

In Unity, GameObjects derived from prefabs don't inherently keep a reference to those prefabs. Furthermore, to prevent serialization issues, when a prefab with a reference to itself is instantiated, the new object gets a reference to itself -- not the original prefab.

This repository provides a few scripts that allow developers to easily create and maintain stable references to source prefabs.

## How to use:

Copy **RememberPrefab.cs** to your scripts folder and **RememberPrefabCustomEditor.cs** to a folder named Editor.

Add **RememberPrefab** as a component to the desired object and select the appropriate prefab reference in the inspector.

![image](https://github.com/skylord-a52/Unity-Remember-Self-Prefab/assets/12107211/38f65f57-bcbc-4021-bcbf-9d1a701fbf07)

Call `GetComponent<RememberPrefab>().Prefab` to get a reference to the prefab in code -- this works in either Edit or Play mode, and the reference always points to the same object no matter what.


## Known Issues

Does not work in builds! Oops!
