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
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using System.Data;
using System.IO;
using System;
using Module.Unity.Custermization;

public class Test2 : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Managers.Resource.LoadAndInisiate("Assets/Prefab/Cube.prefab", null);

            float x = UnityEngine.Random.Range(-5.0f, 5.0f);
            float y = UnityEngine.Random.Range(-5.0f, 5.0f);
            go.transform.position = new Vector3(x, y, 0);
        }

        //byte[] text;
        //Managers.Resource.LoadAsset<TextAsset>("Assets/Automation/Output/Tables/user_character.bytes",
        //    (resAsset) =>
        //    {
        //        text = resAsset.bytes;

        //    },false);

        Managers.Atlas.Register();
        //var handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Cube.prefab");
    }

    public void StartScene()
    {
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }
}

