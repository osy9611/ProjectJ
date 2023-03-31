using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const string tableDataAssetPath = "Assets/Res/Data/ComDataAssets.prefab";
    public const string CommonAtlasPath = "Assets/Res/UI/Prefab/AtlasPrefab/Common.prefab";
    public enum SceneType
    {
        Loading,
        CharacterSelect,
        Game
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

    public enum UIEvent
    {
        Click,
        Drag,
        Up,
        Down
    }

    public enum CostumePart
    {
        Head,
        Cloth,
        Foot
    }

    [Serializable]
    public struct PartDataColor
    {
        public Color color;
    }

}
