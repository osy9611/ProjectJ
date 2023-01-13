using DesignTable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Module.Automation.Generator
{
    public class DataMgrStringData
    {
        //string tableId = info.TableID.ToString();
        //string tableClassName = info.TableName;
        public string enumVariable;
        public string privateVariable;
        public string publicVariable;
        public string registerLoadHandlerFunc;
        public string registerClearHandlerFunc;
        public string loadClassFunc;
        public string clearClassFunc;
        public string setUpRefFunc;

        public void SetData(string tableId, string tableName, List<string> refTable = null)
        {
            string tableFirstUpper = char.ToUpper(tableName[0]) + tableName.Substring(1);
            enumVariable += string.Format(AutomationFormat.designMgrEnumVariableFormat,
                tableName, tableId);
            privateVariable += string.Format(AutomationFormat.designMgrPrivateVariableFormat,
                tableName);
            publicVariable += string.Format(AutomationFormat.designMgrPublicVariableFormat,
                tableName, tableFirstUpper);
            registerLoadHandlerFunc += string.Format(AutomationFormat.designMgrRegisterLoadHandlerFuctionFormat,
                tableId, tableName);
            registerClearHandlerFunc += string.Format(AutomationFormat.designMgrRegisterClearHandlerFuctionFormat,
               tableId, tableName);
            loadClassFunc += string.Format(AutomationFormat.designMgrLoadClassFuctionFormat,
                tableName);
            clearClassFunc += string.Format(AutomationFormat.designMgrClearClassFructionFormat,
                tableName);

            if (refTable == null)
                return;

            for(int i=0,range = refTable.Count;i<range ;++i)
            {
                setUpRefFunc += string.Format(AutomationFormat.designMgrSetUpRefFuctionFormat,
                    tableName, refTable[i]);
            }
                
        }

        public void ResetData()
        {
            enumVariable = "";
            privateVariable = "";
            publicVariable = "";
            registerLoadHandlerFunc = "";
            registerClearHandlerFunc = "";
            loadClassFunc = "";
            clearClassFunc = "";
            setUpRefFunc = "";
        }

        public string GetDataMgrData()
        {
            string variable = privateVariable + publicVariable;
            return string.Format(AutomationFormat.designMgrFormat,
                enumVariable,
                variable,
                registerLoadHandlerFunc,
                 registerClearHandlerFunc,
                 loadClassFunc,
                 clearClassFunc,
                 setUpRefFunc);
        }

        public void ExportDataMgr(string outputPath)
        {
            StreamWriter sw;
            sw = new StreamWriter(Application.dataPath + outputPath + "DataMgr.cs");
            byte[] bytes = Encoding.Default.GetBytes(GetDataMgrData());
            sw.Write(Encoding.UTF8.GetString(bytes));
            sw.Flush();
            sw.Close();
        }
    }

    public class TablePKData
    {
        public string ColumName;
        public string Type;
        public TablePKData(string col, string type)
        {
            ColumName = col;
            Type = type;
        }
    }

    public class TableVarData
    {
        public string ColumName;
        public string Type;
        public string RefTable;
        public TableVarData(string col, string type, string refTable = "")
        {
            ColumName = col;
            Type = type;
            RefTable = refTable;
        }
    }

    public class TableDataInfo
    {
        List<TablePKData> pkData = new List<TablePKData>();
        public List<TablePKData> PkData
        {
            get => pkData;
        }

        List<TableVarData> varData = new List<TableVarData>();
        public List<TableVarData> VarData
        {
            get => varData;
        }

        int tableID = 0;
        public int TableID
        {
            get => tableID;
            set => tableID = value;
        }

        string talbeName;
        public string TableName
        {
            get => talbeName;
            set => talbeName = value;
        }

        public void AddPKData(string ColumName, string Type)
        {
            pkData.Add(new TablePKData(ColumName, Type));
        }

        public void AddVarData(string ColumName, string Type, string refTable = "")
        {

            varData.Add(new TableVarData(ColumName, Type, refTable));
        }

    }

    public class TableFilePathDatas
    {
        public const string ENUM_PATH = "Automation/ExportXmlData/Enum/_data_enum.xml";
        public const string ENUM_OUTPUT_PATH = "Assets/Automation/Output/Enum/DesignEnum";
        public const string TABLE_PATH = "Automation/ExportXmlData/Tables/";
        public const string TABLE_OUTPUT_PATH = "/../Tools/data/table/data/";

        public const string DATAMESSAGES_DLLFILE_NAME = "DesignMessages";
        public const string DLL_OUTPUT_PTAH = "Assets/Automation/Output/Dll";
        public const string DATAMGR_DLLFILE_NAME = "DataMgr";
        public const string DATAMGR_OUTPUT_PATH = "/../Tools/data/table/data/";

        public const string TABLE_DATA_PATH = "/Module/Automation/Experimental/Editor/savedata.json";
    
    }

    public class TalbeSettingData
    {
        public string gmsPath;
        public string comTableAssetPath;

        public static void SaveData(string path,string gmsPath, string comTableAsset)
        {
            TalbeSettingData data = new TalbeSettingData();
            data.comTableAssetPath = comTableAsset;
            data.gmsPath = gmsPath;
            string result = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(path, result);
        }

        public static void LoadData(string path,out string gmsPath,out UnityEngine.Object assetData)
        {
            TalbeSettingData data = new TalbeSettingData();
            string jsonData = System.IO.File.ReadAllText(path);
            data = JsonUtility.FromJson<TalbeSettingData>(jsonData);

            gmsPath = data.gmsPath;
            assetData = AssetDatabase.LoadAssetAtPath(data.comTableAssetPath, typeof(UnityEngine.Object));
        }

        public static TalbeSettingData LoadSettingData(string path)
        {
            TalbeSettingData data = new TalbeSettingData();
            string jsonData = System.IO.File.ReadAllText(path);
            data = JsonUtility.FromJson<TalbeSettingData>(jsonData);

            return data;
        }

    }

}


