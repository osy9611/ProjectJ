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
    [SerializeField] GameObject obj;
    [SerializeField] AssetReference reference;
    DataMessageSerializer serializer = new DataMessageSerializer();

    //private void Start()
    //{
    //    user_characterInfo data = new user_characterInfo();
    //    user_characterInfo data2 = new user_characterInfo();
    //    object char_Id = "1";
    //    object char_classId = 2;
    //    object char_Gender = 3;
    //    object char_model_id = 4;
    //    object char_Common_Attack_Cooltime = 5;
    //    object char_SizeX = 6;
    //    object char_SizeY = 6;
    //    object char_SizeZ = 6;
    //    object char_Move_Speed = 7;
    //    object char_Prefab = "11";
    //    object char_Selection_Prefab = "12";

    //    object[] val = { (int)1, (sbyte)0, (sbyte)0, (sbyte)0, (float)0, (float)0.5, (float)1, (float)0.5, (float)7, "Assets/Res/Art/Character/war/war_male/prefab/war_male.prefab", "Assets/Res/Art/Character/war/war_male/prefab/war_male_selection.prefab" };
    //    object[] val2 = { (int)2, (sbyte)0, (sbyte)0, (sbyte)0, (float)0, (float)0.5, (float)1, (float)0.5, (float)7, "Assets/Res/Art/Character/war/war_male/prefab/war_male.prefab", "Assets/Res/Art/Character/war/war_male/prefab/war_male_selection.prefab" };
    //    object[] val3 = { (int)3, (sbyte)0, (sbyte)0, (sbyte)0, (float)0, (float)0.5, (float)1, (float)0.5, (float)7, "Assets/Res/Art/Character/war/war_male/prefab/war_male.prefab", "Assets/Res/Art/Character/war/war_male/prefab/war_male_selection.prefab" };
    //    object[] val4 = { (int)4, (sbyte)0, (sbyte)0, (sbyte)0, (float)0, (float)0.5, (float)1, (float)0.5, (float)7, "Assets/Res/Art/Character/war/war_male/prefab/war_male.prefab", "Assets/Res/Art/Character/war/war_male/prefab/war_male_selection.prefab" };


    //    using (XmlReader xmlFile = XmlReader.Create(Application.dataPath + "/" + "Automation/ExportXmlData/Tables/" + "_data_user_character.xml"))
    //    {
    //        using (DataSet dataSet = new DataSet())
    //        {
    //            dataSet.ReadXml(xmlFile);
    //        }
    //    }

    //    string typeName = "DesignTable.user_characterInfos";
    //    System.Type sType = System.Type.GetType(typeName);
    //    Debug.Log(Application.dataPath + "/Automation/Output/Dll/DataMgr.dll");
    //    System.Reflection.Assembly dll = System.Reflection.Assembly.LoadFile(Application.dataPath + "/Automation/Output/Dll/DataMgr.dll");
    //    sType = dll.GetType(typeName);
    //    System.Reflection.MethodInfo addMethod = sType.GetMethod("Insert");
    //    object inst = System.Activator.CreateInstance(sType);

    //    bool result = (bool)addMethod.Invoke(inst, val);
    //    bool result2 = (bool)addMethod.Invoke(inst, val2);
    //    bool result3 = (bool)addMethod.Invoke(inst, val3);
    //    bool result4 = (bool)addMethod.Invoke(inst, val4);

    //    string outputpath = Application.dataPath + "/Automation/Output/Tables/test.bytes";

    //    ////serializer.Write((user_characterInfos)inst);
    //    //using (FileStream file = File.Create(outputpath))
    //    //{
    //    //    serializer.Serialize(1011, inst);
    //    //    //File.WriteAllBytes(addresss, buffer);

    //    //}
    //    File.WriteAllBytes(outputpath, serializer.Serialize(inst));

    //    user_characterInfos datas = serializer.Deserialize(1011, File.ReadAllBytes(outputpath)) as user_characterInfos;
    //}

    private void Start()
    {
        LoadData("/user_character.bytes");
    }

    public void LoadData(string dataPath)
    {
        Addressables.LoadAssetAsync<TextAsset>("Assets/Automation/Output/Tables"+dataPath).Completed +=
            (op) =>
            {
                if (op.Status != AsyncOperationStatus.Succeeded)
                    return;

                DataMgr dataMgr = new DataMgr();
                dataMgr.Init();
                dataMgr.LoadData(TableId.user_character, op.Result.bytes);
            };
    }
}

