using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using Module.Automation.Generator;
using System.IO;
using System.Net;
using System.Text;
using System;
using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

namespace Module.Automation.Generator
{
    public delegate void DesignGeneratorDelegate(XmlDocument xml, string outputPath);

    static public class XmlManager
    {
        public static void LoadXML(string address, string outputPath, DesignGeneratorDelegate action = null)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Application.dataPath + "/" + address);
            if (xml == null)
                return;

            action?.Invoke(xml, outputPath);
        }

        public static XmlDocument LoadXML(string address)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Application.dataPath + "/" + address);
            return xml;
        }

        public static void LoadAllXML(string address, string outputPath, DesignGeneratorDelegate action = null)
        {
            XmlDocument xml = new XmlDocument();
            DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/" + address);

            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.FullName.Contains(".meta"))
                    continue;
                xml.Load(file.FullName);
                action?.Invoke(xml, outputPath);
            }
        }

        public static List<XmlDocument> LoadAllXML(string address)
        {
            List<XmlDocument> data = new List<XmlDocument>();

            DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/" + address);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.FullName.Contains(".meta"))
                    continue;

                XmlDocument xml = new XmlDocument();
                xml.Load(file.FullName);
                data.Add(xml);
            }
            return data;
        }
    }

    public class EnumGenerator
    {
        string designEnums;
        string enumDatas = "";

        public void Load(string address, string outputPath)
        {
            XmlManager.LoadXML(address, outputPath, Save);
        }

        public void Save(XmlDocument xml, string outputPath)
        {
            if (xml == null)
                return;

            XmlNodeList nodes = xml.SelectNodes("Enum/record");

            foreach (XmlNode node in nodes)
            {
                if (node.SelectSingleNode("ENUM_VALUE").InnerText == "-")
                {

                    if (enumDatas != "")
                    {
                        enumDatas += "    }\n" + string.Format(AutomationFormat.enumSummaryFormat, node.SelectSingleNode("GROUP_ID").InnerText, node.SelectSingleNode("COMMENT").InnerText, node.SelectSingleNode("ENUM_NAME").InnerText);

                    }
                    else
                    {
                        enumDatas += string.Format(AutomationFormat.enumSummaryFormat, node.SelectSingleNode("GROUP_ID").InnerText, node.SelectSingleNode("COMMENT").InnerText, node.SelectSingleNode("ENUM_NAME").InnerText);

                    }
                }
                else
                {
                    string enumCommnet = "";
                    if (node.SelectSingleNode("COMMENT").InnerText != "-")
                    {
                        enumCommnet = "//" + node.SelectSingleNode("COMMENT").InnerText;
                    }
                    enumDatas += string.Format(AutomationFormat.enumFormat, node.SelectSingleNode("ENUM_NAME").InnerText, node.SelectSingleNode("ENUM_VALUE").InnerText, enumCommnet);
                }
            }

            if (enumDatas.Substring(enumDatas.Length - 1, 1) != "}")
            {
                enumDatas += "    }";
            }


            designEnums += string.Format(AutomationFormat.designEnumFormat, enumDatas, System.Text.Encoding.UTF8);
            Debug.Log(designEnums);
            StreamWriter sw;

            sw = new StreamWriter(outputPath + ".cs");

            byte[] bytes = Encoding.Default.GetBytes(designEnums);
            sw.Write(Encoding.UTF8.GetString(bytes));
            sw.Flush();
            sw.Close();
        }
    }


    public class TableGenerator
    {

        Dictionary<int, TableDataInfo> tableInfoData = new Dictionary<int, TableDataInfo>();
        DataMgrStringData dataMgrData;
        public void Load(string address, string outputPath, string dataMgrOutputPath, string readAllDataPath,string comAssetPath)
        {
            XmlDocument xml = XmlManager.LoadXML(address);
            List<XmlDocument> xmlDatas = XmlManager.LoadAllXML(readAllDataPath);
            foreach (XmlDocument data in xmlDatas)
            {
                GetTableInfo(data);
            }

            XmlNodeList nodes = xml.SelectNodes(xml.DocumentElement.Name + "/verify");
            SaveInfoData(tableInfoData[int.Parse(nodes[0].Attributes["TableId"].Value)], outputPath);
            AddTableAsset(tableInfoData[int.Parse(nodes[0].Attributes["TableId"].Value)], comAssetPath);

            foreach (TableDataInfo data in tableInfoData.Values)
            {
                SaveDataMgrData(data);
            }
            ExportDataMgr(dataMgrOutputPath);
        }

        public void LoadAll(string address, string outputPath, string dataMgrOutputPath, string comAssetPath)
        {
            List<XmlDocument> xmlDatas = XmlManager.LoadAllXML(address);

            foreach (XmlDocument data in xmlDatas)
            {
                GetTableInfo(data);
            }

            foreach (TableDataInfo data in tableInfoData.Values)
            {
                SaveInfoData(data, outputPath);
                SaveDataMgrData(data);
                AddTableAsset(data, comAssetPath);
            }

            Debug.Log(dataMgrOutputPath);

            ExportDataMgr(dataMgrOutputPath);
        }


        public void GetTableInfo(XmlDocument xml)
        {
            if (xml == null)
                return;
            TableDataInfo info = new TableDataInfo();
            XmlNodeList nodes = xml.SelectNodes(xml.DocumentElement.Name + "/verify");

            info.TableName = xml.DocumentElement.Name;
            info.TableID = int.Parse(nodes[0].Attributes["TableId"].Value);
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["PK"].Value == "Y")
                {
                    info.AddPKData(node.Attributes["ColumnName"].Value, CheckDataType(node.Attributes["Type"].Value));
                }

                if (node.Attributes["StringHashIds"].Value != "-")
                {
                    info.AddVarData(
                        node.Attributes["ColumnName"].Value,
                        CheckDataType(node.Attributes["Type"].Value), node.Attributes["StringHashIds"].Value);
                }
                else
                {
                    info.AddVarData(node.Attributes["ColumnName"].Value
                        , CheckDataType(node.Attributes["Type"].Value));
                }
            }


            if (!tableInfoData.ContainsKey((int.Parse(nodes[0].Attributes["TableId"].Value))))
            {
                tableInfoData.Add(int.Parse(nodes[0].Attributes["TableId"].Value), info);
            }
        }

        public void SaveInfoData(TableDataInfo info, string outputPath)
        {
            if (info == null)
                return;

            //talbe Info
            string talbeInfoData = "";
            string talbeInfoClassData = "";
            string tableInfoClassVariablesData = "";
            string tableInfoClassConstructParamsData = "";
            string tableInfoClassConstructData = "";


            foreach (TableVarData data in info.VarData)
            {
                tableInfoClassVariablesData += string.Format(AutomationFormat.designTalbeInfoClassVariablesFormat,
                    data.Type, data.ColumName);
                tableInfoClassConstructParamsData += data.Type + " " + data.ColumName + (info.VarData[info.VarData.Count - 1] == data ? "" : ",");
                tableInfoClassConstructData += string.Format(AutomationFormat.designTalbeInfoClassContructParamsFormat, data.ColumName);

                if (data.RefTable != "")
                {
                    tableInfoClassVariablesData += string.Format(AutomationFormat.designTalbeInfoClassVariablesFormat,
                    tableInfoData[int.Parse(data.RefTable)].TableName + "Info", data.ColumName + "_ref");
                }
            }

            talbeInfoClassData += string.Format(AutomationFormat.designTalbeInfoClassFormat,
                info.TableName, tableInfoClassVariablesData, tableInfoClassConstructParamsData, tableInfoClassConstructData);

            talbeInfoData += string.Format(AutomationFormat.designTableInfoFormat,
               talbeInfoClassData + SaveInfosData(info));


            StreamWriter sw;

            sw = new StreamWriter(Application.dataPath + outputPath + info.TableName + "Info" + ".cs");

            byte[] bytes = Encoding.Default.GetBytes(talbeInfoData);
            sw.Write(Encoding.UTF8.GetString(bytes));
            sw.Flush();
            sw.Close();
        }

        public string SaveInfosData(TableDataInfo info)
        {
            //talbe Info
            string tableClassName = info.TableName;
            string tableClassVarTypeName = "";
            string tablePKVarName = "";
            string tableClassVarName = "";
            string tablePKVarTypeName = "";
            string tableGetIdRullFuctionCount = "";
            string tableSetUpRefFuction = "";

            foreach (TableVarData data in info.VarData)
            {
                tableClassVarTypeName += data.Type + " " + data.ColumName + (info.VarData[info.VarData.Count - 1] == data ? "" : ",");
                tableClassVarName += data.ColumName + (info.VarData[info.VarData.Count - 1] == data ? "" : ",");

                if (data.RefTable != "")
                {
                    tableSetUpRefFuction += string.Format(AutomationFormat.designTableInfosSetupItemIdFuctionFormat, tableInfoData[int.Parse(data.RefTable)].TableName,
                                            info.TableName, data.ColumName, data.Type);
                }
            }

            foreach (TablePKData data in info.PkData)
            {
                tablePKVarTypeName += data.Type + " " + data.ColumName + (info.PkData[info.PkData.Count - 1] == data ? "" : ",");
                tablePKVarName += data.ColumName + (info.PkData[info.PkData.Count - 1] == data ? "" : ",");

                tableGetIdRullFuctionCount += string.Format(AutomationFormat.designTableInfosGetIdRullFuctionCountFormat,
                    data.Type);
            }


            string result = string.Format(AutomationFormat.designTalbeInfosClassFormat,
                tableClassName, tableClassVarTypeName, tablePKVarName, tableClassVarName,
                tablePKVarTypeName, tableGetIdRullFuctionCount, tableSetUpRefFuction);

            return result;
        }

        public void SaveDataMgrData(TableDataInfo info)
        {
            string tableId = info.TableID.ToString();
            string tableClassName = info.TableName;
            List<string> refTable = new List<string>();
            foreach (TableVarData data in info.VarData)
            {
                if (data.RefTable != "")
                {
                    refTable.Add(tableInfoData[int.Parse(data.RefTable)].TableName);
                }
            }

            if (dataMgrData == null)
            {
                dataMgrData = new DataMgrStringData();
            }

            dataMgrData.SetData(tableId, tableClassName, refTable);
        }

        public void ExportDataMgr(string outputPath)
        {
            dataMgrData.ExportDataMgr(outputPath);
        }
        public void AddTableAsset(TableDataInfo info, string path)
        {
            GameObject go = AssetDatabase.LoadAssetAtPath(path,typeof(UnityEngine.Object)) as GameObject;
            if (go == null)
                return;

            ComTableAsset comTableAsset = go.GetComponent<ComTableAsset>();
            if(comTableAsset == null) 
                return;

            TableAssetInfo data = new TableAssetInfo(info.TableID, "_data_" + info.TableName);
            comTableAsset.Add(data);
        }

        public string CheckDataType(string type)
        {
            switch (type)
            {
                case "Int8":
                    return "sbyte";
                case "Int16":
                    return "short";
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
                case "UInt8":
                    return "byte";
                case "UInt16":
                    return "ushort";
                case "UInt32":
                    return "uint";
                case "UInt64":
                    return "ulong";
                case "string":
                case "bool":
                case "float":
                case "double":
                    return type;
            }

            return "";
        }
    }

}
