using Module.Unity.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ComScene : MonoBehaviour
{
    public AssetReference BgmSound;

    protected virtual void Awake()
    {
        Managers.Resource.LoadAsset<AudioClip>(BgmSound,
           (result) =>
           {
               if (result != null)
               {
                   Managers.Sound.Play(result, Sound.Bgm, 1);
               }
           });
    }

    protected virtual void OnDestroy()
    {
        Managers.Resource.Release(BgmSound);
    }
}
