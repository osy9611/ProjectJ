using Module.Unity.Sound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ComScene : MonoBehaviour
{
    public AssetReference BgmSound;

    protected virtual void Start()
    {
        if (BgmSound.ToString() == "[]")
            return;
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

    public virtual void StartScene(int id)
    {
        bool result = System.Enum.IsDefined(typeof(Define.SceneType), id);
        if (result)
            Managers.Scene.LoadScene((Define.SceneType)id);
        else
            Debug.LogError("scene Number incorrect");
    }

}
