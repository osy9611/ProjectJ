using Module.Unity.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ComStart : ComScene
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void StartScene()
    {
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }
    protected override void OnDestroy()
    {
        Managers.Resource.Release(BgmSound);
    }
}
