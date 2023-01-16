using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Automation.Generator
{
    class AutomationFormat
    {
        //{0} enum ������ ���
        public static string designEnumFormat =
@"namespace DesignEnum
{{
{0}
}}
";
        //{0} enum ID
        //{1} enum �ڸ�Ʈ
        //{2} enum ��
        public static string enumSummaryFormat =
@"    /// <summary> 
    /// ID : {0}
    /// {1}
    /// </summary> 
    public enum {2}
    {{
";

        //{0} enum name
        //{1} enum value
        //{2} enum comment
        public static string enumFormat =
@"        {0} = {1}, {2}
";

        //{0} talbe class
        public static string designTableInfoFormat =
@"using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{{
    [ProtoContract]
    {0}
}}
";
        //{0} : table class name
        //{1} : table class variables
        //{2} : class parameter ������ �Լ�
        //{3} : class parameter �Ҵ�
        public static string designTalbeInfoClassFormat =
@"public class {0}Info
    {{
{1}
        public {0}Info()
        {{
        }}

        public {0}Info({2})
        {{
{3}
        }}
    }}
";
        //{0} : table class variable type
        //{1} : table class variable name
        //{2} : protoNumber
        public static string designTalbeInfoClassVariablesFormat =
@"        [ProtoMember({2})] 
        public {0} {1};
";
        //{0} : table class variable type
        //{1} : table class variable name
        //{2} : protoNumber
        public static string designTalbeInfoClassRefTableVariablesFormat =
@"          public {0} {1};        
";

        //{0} : table class variable name
        public static string designTalbeInfoClassContructParamsFormat =
@"            this.{0} = {0};
";

        //{0} : table class name
        //{1} : table class variable type & name
        //{2} : table pk variable name
        //{3} : table class variable name
        //{4} : table pk variable type & name
        //{5} : designTableInfosGetIdRullFuctionCountFormat
        public static string designTalbeInfosClassFormat =
@"      [ProtoContract]
    public class {0}Infos
    {{
        [ProtoMember(1)]
        public List<{0}Info> m_data = new List<{0}Info>();
        public Dictionary<ArraySegment<byte>, {0}Info> datas = new Dictionary<ArraySegment<byte>, {0}Info>();
      
        public bool Insert({1})
        {{
            ArraySegment<byte> bytes = GetIdRule({2});
            if (datas.ContainsKey(bytes))
                return false;

            datas.Add(bytes,new {0}Info({3}));
            m_data.Add(new {0}Info({3}));
            return true;
        }}

        public {0}Info Get({4})
        {{
            {0}Info value = null;
            
            if(datas.TryGetValue(GetIdRule({2}),out value))
                return value;
            
            return null;
        }}

        public ArraySegment<byte> GetIdRule({4})
        {{
            ushort count = 0;
            {5}

            if (count == 0)
               return null;

            byte[] bytes = new byte[count];
            return bytes;
        }}

        {6}

    }}
";

        //{0} : table variable type
        public static string designTableInfosGetIdRullFuctionCountFormat =
@"          count += sizeof({0});
";

        //{0} : TableVarData class RefTable
        //{1} : table name
        //{2} : TableVarData class ColumName
        //{3} : TableVarData class Type
        public static string designTableInfosSetupItemIdFuctionFormat =
@"public void SetupRef_item_Id({0}Infos infos)
        {{
            foreach({1}Info data in datas.Values)
            {{
                if(data.{2} != -1)
                {{
                    data.{2}_ref = infos.Get(({3})data.{2});
                }}
            }}
        }}
";

        //{0} : designMgrEnumVariableFormat
        //{1} : designMgrVariableFormat & designMgrPublicVariableFormat
        //{2} : designMgrRegisterLoadHandlerFuctionFormat
        //{3} : designMgrRegisterClearHandlerFuctionFormat
        //{4} : designMgrLoadClassFuctionFormat
        //{5} : designMgrClearClassFructionFormat
        //{6} : designMgrSetUpRefFuctionFormat
        public static string designMgrFormat =
@"using System.Collections.Generic;
using System.IO;
namespace DesignTable
{{
    public enum TableId
    {{
        {0}
    }}
    
    public class DataMgr
    {{
        private delegate void LoadHandler();
        private delegate void ClearHandler();

        private Dictionary<int, DataMgr.LoadHandler> loadHandlerList = new Dictionary<int, LoadHandler>();
        private Dictionary<int, DataMgr.ClearHandler> clearHandlerList = new Dictionary<int, ClearHandler>();
        private bool isCallInit = false;
        {1}
        
        public void Init()
        {{
            if (isCallInit)
                return;

            RegisterLoadHandler();
            RegisterClearHandler();
            isCallInit = true;
        }}

        public void LoadData(TableId dataType)
        {{
            loadHandlerList[(int)dataType]();
        }}

        public void ClearData(TableId[] dataTypes)
        {{
            foreach (int dataType in dataTypes)
            {{
                clearHandlerList[dataType]();
            }}
        }}

        public void ClearData(TableId dataTypes)
        {{
             clearHandlerList[(int)dataTypes]();
        }}
        
        public void ClearDataAll()
        {{
            foreach (DataMgr.ClearHandler clearHandler in clearHandlerList.Values)
            {{
                clearHandler();
            }}
        }}

        private void RegisterLoadHandler()
        {{
            {2}
        }}  

        private void RegisterClearHandler()
        {{
            {3}
        }}
        
        {4}
        
        {5}

        public void SetUpRef()
        {{
            {6}
        }}
        
    }}
}}
";

        //{0} : table name
        //{1} : table id
        public static string designMgrEnumVariableFormat =
@"{0} = {1},
";
        //{0} : talbe name
        public static string designMgrPrivateVariableFormat =
@"private {0}Infos {0}Infos;
"; 

        //{0} : table name
        //{1} : table name(앞자리는 대문자)
        public static string designMgrPublicVariableFormat =
@"public {0}Infos {1}Infos => {0}Infos;
";

        //{0} : table id
        //{1} : table name
        public static string designMgrRegisterLoadHandlerFuctionFormat =
@"loadHandlerList.Add({0}, new DataMgr.LoadHandler(Load{1}Infos));
";

        //{0} : table id
        //{1} : table name
        public static string designMgrRegisterClearHandlerFuctionFormat =
@"clearHandlerList.Add({0}, ClearData{1}Infos);
";
        //{0} : table name
        public static string designMgrLoadClassFuctionFormat =
@"private void Load{0}Infos()
{{
    {0}Infos = new {0}Infos();
}}
";
        //{0} : table name
        public static string designMgrClearClassFructionFormat =
@"private void ClearData{0}Infos()
{{
    if({0}Infos != null)
        {0}Infos=null;
}}
";

        //{0} : table name
        //{1} : TableVarData class RefTable
        public static string designMgrSetUpRefFuctionFormat =
@"{0}Infos.SetupRef_item_Id({1}Infos);
";



    }

}