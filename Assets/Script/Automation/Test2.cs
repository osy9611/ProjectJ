using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Xml;
using JetBrains.Annotations;
using System.Text;
using UnityEngine.Networking.Match;
using DesignTable;
using ProtoBuf;
using Unity.VisualScripting;
using System.Data;
using System.IO;
using System;
using Module.Unity.Custermization;
using ProjectJ;
using Module.Unity.Core;
using Module.Unity.Utils;
using Module.Automation;

[DefaultExecutionOrder(-1000)]
public class Test2 : MonoBehaviour
{
    public ComDataAssets tableAsset;
    public bool UseAddressable = false;
    public GameObject Player;

    private void Start()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject go = Managers.Resource.LoadAndInisiate("Assets/Prefab/Cube.prefab", null);

        //    float x = UnityEngine.Random.Range(-5.0f, 5.0f);
        //    float y = UnityEngine.Random.Range(-5.0f, 5.0f);
        //    go.transform.position = new Vector3(x, y, 0);
        //}

        //byte[] text;
        //Managers.Resource.LoadAsset<GameObject>("Assets/Res/Data/ComDataAssets.prefab",
        //    (resAsset) =>
        //    {
        //        Debug.Log(resAsset);

        //    }, false);

        //Managers.Atlas.Register();
        //var handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Cube.prefab");
       
        if (!UseAddressable)
        {
            Player.SetActive(false);
            Managers.Data.OnLoadData((result) =>
            {
                Player.SetActive(true);
            });
        }
    }

    public void StartScene()
    {
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }

    private void DoMainProcess()
    {
        Module.Unity.Utils.Util.Wait(this, 1, null);
    }
}

