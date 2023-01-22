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
using Module.Unity.Managers;

public class Test : MonoBehaviour
{
    public SceneManager sceneManager;
    private void Start()
    {
        Managers.Resource.LoadAndInisiate("Assets/Prefab/Cube.prefab", null,true,false);

        Managers.Resource.LoadAsset<TextAsset>("Assets/Automation/Output/Tables/user_character.bytes",
            (resAsset) =>
            {
                bool success = resAsset != null;

            });
        Managers.Resource.ReleaseAll();

        Managers.Atlas.Register();

    }

    public void StartScene()
    {
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }
}

