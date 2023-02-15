using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const string tableDataAssetPath = "Assets/Res/Data/ComDataAssets.prefab";
    public enum SceneType
    {
        Loading,
        Game
    }

    [Flags]
    public enum InputEvnetType
    {
        None = 0,
        Start = 1<<0,
        Performed = 1<<1,
        Cancel = 1<<2
    }

    public enum ObjectState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Death,
        Spawn
    }


}
