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

public class Test : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] AssetReference reference;
    DataMessageSerializer serializer = new DataMessageSerializer();

    private void Start()
    {
        user_characterInfo data = new user_characterInfo();
        user_characterInfos datas = new user_characterInfos();
        user_characterInfo data2 = new user_characterInfo();
        object char_Id = "1";
        object char_classId = 2;
        object char_Gender = 3;
        object char_model_id = 4;
        object char_Common_Attack_Cooltime = 5;
        object char_SizeX = 6;
        object char_SizeY = 6;
        object char_SizeZ = 6;
        object char_Move_Speed = 7;
        object char_Prefab = "11";
        object char_Selection_Prefab = "12";

        object[] val = { (int)1, (sbyte)0, (sbyte)0, (sbyte)0, (float)0, (float)0.5, (float)1, (float)0.5, (float)7, "Assets/Res/Art/Character/war/war_male/prefab/war_male.prefab", "Assets/Res/Art/Character/war/war_male/prefab/war_male_selection.prefab" };

        using (XmlReader xmlFile = XmlReader.Create(Application.dataPath + "/" + "Automation/ExportXmlData/Tables/" + "_data_user_character.xml"))
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.ReadXml(xmlFile);
            }
        }

        string typeName = "DesignTable.user_characterInfos";
        System.Type sType = System.Type.GetType(typeName);
        Debug.Log(Application.dataPath + "/Automation/Output/Dll/DataMgr.dll");
        System.Reflection.Assembly dll = System.Reflection.Assembly.LoadFile(Application.dataPath + "/Automation/Output/Dll/DataMgr.dll");
        sType = dll.GetType(typeName);
        System.Reflection.MethodInfo addMethod = sType.GetMethod("Insert");
        object inst = System.Activator.CreateInstance(sType);

        bool result = (bool)addMethod.Invoke(inst, val);
        serializer.Write((user_characterInfos)inst);

    }

}

