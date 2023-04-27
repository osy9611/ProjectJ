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
    bool firstInit = true;
    public void Init()
    {
        if (loadingBar != null)
            return;
        GameObject go = Managers.Resource.LoadAndInisiate("Assets/Res/UI/Prefab/LoadingBar.prefab");
        loadingBar = ComponentUtil.FindChild<Image>(go, "LoadingBar");
    }

    public void LoadScene(Define.SceneType type)
    {
        Managers.UI.CloseSceneUI();
        if (Managers.Object.MyActor != null)
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
            case Define.SceneType.Field:
                yield return CoLoadFieldScene();
                break;
            case Define.SceneType.Dugeon:
                yield return CoLoadDungeon();
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
        if (firstInit)
        {
#if UNITY_ANDROID || UNITY_IOS
        comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/MobileCanvas.prefab");

#else
            comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/PCCanvas.prefab");
#endif
            //Player Load
            Managers.Object.InitPlayer(true);
            //Quest Init
            Managers.Quest.AddQuest<QuestData_Monster>(0);
            firstInit = false;
        }
        else
        {
#if UNITY_ANDROID || UNITY_IOS
        comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/MobileCanvas.prefab",false);

#else
            comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/PCCanvas.prefab", false);
#endif
            //Player Load
            Managers.Object.InitPlayer(false);
        }

        if (comBattleMode != null)
        {
            Managers.Hud.SetHud(comBattleMode);
        }

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
        Managers.Object.MyActor.Creature.transform.rotation = Quaternion.Euler(0, comCostumeMode.RotationzY, 0);
    }

    public IEnumerator CoLoadDungeon()
    {
        bool success = false;

        yield return Managers.Resource.CoLoadSceneAsync("Dungeon", LoadSceneMode.Single, (result) =>
        {
            success = result;
        }, loadingBar);


        if (!success)
        {
            yield break;
        }

        ComBattleMode comBattleMode = null;
#if UNITY_ANDROID || UNITY_IOS
        comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/MobileCanvas.prefab",false);

#else
        comBattleMode = Managers.UI.ShowSceneUI<ComBattleMode>("Assets/Res/UI/Prefab/DynamicLoading/PCCanvas.prefab", false);
#endif
        if (comBattleMode != null)
        {
            Managers.Hud.SetHud(comBattleMode);
        }

        //Player Load
        Managers.Object.InitPlayer();

        //Monster Load
        Managers.Object.LoadMonster(DesignEnum.FieldType.Dungeon);

        //Quest Init
        Managers.Quest.AddQuest<QuestData_Monster>(1);

        Managers.UI.GetElem<ComUIElemQuest>().AddQuestUI();

    }
}
