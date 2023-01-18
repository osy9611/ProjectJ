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


public class Test : MonoBehaviour
{
    private void Start()
    {
        ModuleManagers.Resource.Inisiate("Assets/Prefab/Cube.prefab",null,true);
    }
}

