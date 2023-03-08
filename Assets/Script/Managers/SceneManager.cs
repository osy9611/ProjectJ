using DesignTable;
using Module.Unity.Core;
using Module.Unity.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager
{
    Image loadingBar;
    public void Init()
    {
        if (loadingBar != null)
            return;
        GameObject go = Managers.Resource.LoadAndInisiate("Assets/Res/UI/Prefab/LoadingBar.prefab");
        loadingBar = Util.FindChild<Image>(go, "LoadingBar");
    }

    public void LoadScene(Define.SceneType type)
    {
        ComLoader.s_Root.StartCoroutine(CoLoadingScene(type));
        switch(type)
        {
            case Define.SceneType.Game:
                //ComLoader.s_Root.StartCoroutine(CoLoadGameScene());
                break;
        }    
    }

    IEnumerator CoLoadingScene(Define.SceneType type)
    {
        bool success = false;
        yield return Managers.Resource.CoLoadSceneAsync("Loading", LoadSceneMode.Single, (result) =>
        {
            success = result;
        });

        if (!success)
        {
            yield break;
        }

        Init();

        yield return Managers.Data.CoLoadData();
        yield return CoLoadFieldScene();
    }

    public IEnumerator CoLoadFieldScene()
    {
        bool success = false;

        yield return Managers.Resource.CoLoadSceneAsync("Field", LoadSceneMode.Single, (result) =>
        {
            success = result;
        }, loadingBar);

      
        if (!success)
        {
            yield break;
        }

        //UI Load
        ComBattleMode comBattleMode = null;
#if UNITY_ANDROID || UNITY_IOS
        comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/MobileCanvas.prefab");

#else
        comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/PCCanvas.prefab");
#endif
        if (comBattleMode != null)
        {
            Managers.Hud.SetHud(comBattleMode);
        }

        //Player Load
        Managers.Object.LoadPlayer(0,true);

        //Monster Load
        Managers.Object.LoadMonster(DesignEnum.FieldType.Field);

        
    }
}
