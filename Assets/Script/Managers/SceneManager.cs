using DesignTable;
using Module.Unity.Core;
using Module.Unity.Custermization;
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
        Managers.UI.CloseSceneUI();
        if(Managers.Object.MyActor != null)
                Managers.Object.MyActor.Creature.gameObject.SetActive(false);

        ComLoader.Root.StartCoroutine(CoLoadingScene(type));
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

        switch (type)
        {
            case Define.SceneType.CharacterSelect:
                yield return CoLoadCharectorSelectScene();
                break;
            case Define.SceneType.Game:
                yield return CoLoadFieldScene();
                break;
        }      
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
        ComCostumeMode costumeMode = Managers.UI.Get<ComCostumeMode>();
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
        Managers.Object.InitPlayer();

        //Monster Load
        Managers.Object.LoadMonster(DesignEnum.FieldType.Field);

        
    }

    public IEnumerator CoLoadCharectorSelectScene()
    {
        bool success = false;

        yield return Managers.Resource.CoLoadSceneAsync("CharacterSelection", LoadSceneMode.Single, (result) =>
        {
            success = result;
        }, loadingBar);


        if (!success)
        {
            yield break;
        }

        //Player Load
        Managers.Object.LoadPlayer(0, true);

        //UI Load
        ComCostumeMode comCostumeMode = null;
        comCostumeMode = Managers.UI.ShowSceneUI<ComCostumeMode>("Assets/Res/UI/Prefab/DynamicLoading/CostumeCanvas.prefab");


        Managers.Object.MyActor.Creature.transform.position = comCostumeMode.StartPoint;
        Managers.Object.MyActor.Creature.transform.rotation = Quaternion.Euler(0, comCostumeMode.RotationzY,0);
    }
}
