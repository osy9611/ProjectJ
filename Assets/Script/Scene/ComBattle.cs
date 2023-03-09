using Module.Unity.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ComBattle : ComScene 
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        Managers.Resource.Release(BgmSound);
    }
}
