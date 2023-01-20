using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Xml;
using JetBrains.Annotations;
using Module.Automation.Generator;
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
    private void Start()
    {
        Managers.Resource.Inisiate("Assets/Prefab/Cube.prefab", null,true);

        Managers.Resource.LoadAsset<TextAsset>("Assets/Automation/Output/Tables/user_character.bytes",
            (resAsset) =>
            {
                bool success = resAsset != null;

            });
        Managers.Resource.ReleaseAll();

        Managers.Atlas.Register();

    }
}

