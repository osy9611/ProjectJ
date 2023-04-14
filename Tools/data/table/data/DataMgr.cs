using System.Collections.Generic;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System;
namespace DesignTable
{
    public class DataComparer :  IEqualityComparer<ArraySegment<byte>>
    {
        public bool Equals([AllowNull] ArraySegment<byte> x, [AllowNull] ArraySegment<byte> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode([DisallowNull] ArraySegment<byte> obj)
        {
            if(obj == null) throw new ArgumentNullException("obj");
            return obj.Sum(y => y);
        }
    }

    public enum TableId
    {
        buff = 1014,
monster_boss = 1018,
monster_deploy = 1019,
monster_master = 1016,
monster_normal = 1017,
passive = 1021,
quest = 1020,
skill = 1013,
skill_effect = 1015,
user_character = 1011,
user_character2 = 1012,

    }
    
    public class DataMgr
    {
        private delegate void LoadHandler(byte[] data);
        private delegate void ClearHandler();

        private Dictionary<int, DataMgr.LoadHandler> loadHandlerList = new Dictionary<int, LoadHandler>();
        private Dictionary<int, DataMgr.ClearHandler> clearHandlerList = new Dictionary<int, ClearHandler>();
        private bool isCallInit = false;
        DataMessageSerializer serializer = new DataMessageSerializer();

        private buffInfos buffInfos;
private monster_bossInfos monster_bossInfos;
private monster_deployInfos monster_deployInfos;
private monster_masterInfos monster_masterInfos;
private monster_normalInfos monster_normalInfos;
private passiveInfos passiveInfos;
private questInfos questInfos;
private skillInfos skillInfos;
private skill_effectInfos skill_effectInfos;
private user_characterInfos user_characterInfos;
private user_character2Infos user_character2Infos;
public buffInfos BuffInfos => buffInfos;
public monster_bossInfos Monster_bossInfos => monster_bossInfos;
public monster_deployInfos Monster_deployInfos => monster_deployInfos;
public monster_masterInfos Monster_masterInfos => monster_masterInfos;
public monster_normalInfos Monster_normalInfos => monster_normalInfos;
public passiveInfos PassiveInfos => passiveInfos;
public questInfos QuestInfos => questInfos;
public skillInfos SkillInfos => skillInfos;
public skill_effectInfos Skill_effectInfos => skill_effectInfos;
public user_characterInfos User_characterInfos => user_characterInfos;
public user_character2Infos User_character2Infos => user_character2Infos;

        
        public virtual void Init()
        {
            if (isCallInit)
                return;

            RegisterLoadHandler();
            RegisterClearHandler();
            isCallInit = true;
        }

        public void LoadData(TableId dataType,byte[] data)
        {
            loadHandlerList[(int)dataType](data);
        }

        public void ClearData(TableId[] dataTypes)
        {
            foreach (int dataType in dataTypes)
            {
                clearHandlerList[dataType]();
            }
        }

        public void ClearData(TableId dataTypes)
        {
             clearHandlerList[(int)dataTypes]();
        }
        
        public void ClearDataAll()
        {
            foreach (DataMgr.ClearHandler clearHandler in clearHandlerList.Values)
            {
                clearHandler();
            }
        }

        private void RegisterLoadHandler()
        {
            loadHandlerList.Add(1014, new DataMgr.LoadHandler(LoadbuffInfos));
loadHandlerList.Add(1018, new DataMgr.LoadHandler(Loadmonster_bossInfos));
loadHandlerList.Add(1019, new DataMgr.LoadHandler(Loadmonster_deployInfos));
loadHandlerList.Add(1016, new DataMgr.LoadHandler(Loadmonster_masterInfos));
loadHandlerList.Add(1017, new DataMgr.LoadHandler(Loadmonster_normalInfos));
loadHandlerList.Add(1021, new DataMgr.LoadHandler(LoadpassiveInfos));
loadHandlerList.Add(1020, new DataMgr.LoadHandler(LoadquestInfos));
loadHandlerList.Add(1013, new DataMgr.LoadHandler(LoadskillInfos));
loadHandlerList.Add(1015, new DataMgr.LoadHandler(Loadskill_effectInfos));
loadHandlerList.Add(1011, new DataMgr.LoadHandler(Loaduser_characterInfos));
loadHandlerList.Add(1012, new DataMgr.LoadHandler(Loaduser_character2Infos));

        }  

        private void RegisterClearHandler()
        {
            clearHandlerList.Add(1014, ClearDatabuffInfos);
clearHandlerList.Add(1018, ClearDatamonster_bossInfos);
clearHandlerList.Add(1019, ClearDatamonster_deployInfos);
clearHandlerList.Add(1016, ClearDatamonster_masterInfos);
clearHandlerList.Add(1017, ClearDatamonster_normalInfos);
clearHandlerList.Add(1021, ClearDatapassiveInfos);
clearHandlerList.Add(1020, ClearDataquestInfos);
clearHandlerList.Add(1013, ClearDataskillInfos);
clearHandlerList.Add(1015, ClearDataskill_effectInfos);
clearHandlerList.Add(1011, ClearDatauser_characterInfos);
clearHandlerList.Add(1012, ClearDatauser_character2Infos);

        }
        
        private void LoadbuffInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        buffInfos = serializer.Deserialize(1014,data) as buffInfos;
        buffInfos.Initialize();
    }
}
private void Loadmonster_bossInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        monster_bossInfos = serializer.Deserialize(1018,data) as monster_bossInfos;
        monster_bossInfos.Initialize();
    }
}
private void Loadmonster_deployInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        monster_deployInfos = serializer.Deserialize(1019,data) as monster_deployInfos;
        monster_deployInfos.Initialize();
    }
}
private void Loadmonster_masterInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        monster_masterInfos = serializer.Deserialize(1016,data) as monster_masterInfos;
        monster_masterInfos.Initialize();
    }
}
private void Loadmonster_normalInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        monster_normalInfos = serializer.Deserialize(1017,data) as monster_normalInfos;
        monster_normalInfos.Initialize();
    }
}
private void LoadpassiveInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        passiveInfos = serializer.Deserialize(1021,data) as passiveInfos;
        passiveInfos.Initialize();
    }
}
private void LoadquestInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        questInfos = serializer.Deserialize(1020,data) as questInfos;
        questInfos.Initialize();
    }
}
private void LoadskillInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        skillInfos = serializer.Deserialize(1013,data) as skillInfos;
        skillInfos.Initialize();
    }
}
private void Loadskill_effectInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        skill_effectInfos = serializer.Deserialize(1015,data) as skill_effectInfos;
        skill_effectInfos.Initialize();
    }
}
private void Loaduser_characterInfos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        user_characterInfos = serializer.Deserialize(1011,data) as user_characterInfos;
        user_characterInfos.Initialize();
    }
}
private void Loaduser_character2Infos(byte[] data)
{
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
        user_character2Infos = serializer.Deserialize(1012,data) as user_character2Infos;
        user_character2Infos.Initialize();
    }
}

        
        private void ClearDatabuffInfos()
{
    if(buffInfos != null)
        buffInfos=null;
}
private void ClearDatamonster_bossInfos()
{
    if(monster_bossInfos != null)
        monster_bossInfos=null;
}
private void ClearDatamonster_deployInfos()
{
    if(monster_deployInfos != null)
        monster_deployInfos=null;
}
private void ClearDatamonster_masterInfos()
{
    if(monster_masterInfos != null)
        monster_masterInfos=null;
}
private void ClearDatamonster_normalInfos()
{
    if(monster_normalInfos != null)
        monster_normalInfos=null;
}
private void ClearDatapassiveInfos()
{
    if(passiveInfos != null)
        passiveInfos=null;
}
private void ClearDataquestInfos()
{
    if(questInfos != null)
        questInfos=null;
}
private void ClearDataskillInfos()
{
    if(skillInfos != null)
        skillInfos=null;
}
private void ClearDataskill_effectInfos()
{
    if(skill_effectInfos != null)
        skill_effectInfos=null;
}
private void ClearDatauser_characterInfos()
{
    if(user_characterInfos != null)
        user_characterInfos=null;
}
private void ClearDatauser_character2Infos()
{
    if(user_character2Infos != null)
        user_character2Infos=null;
}


        public void SetUpRef()
        {
            skillInfos.SetupRef_item_Id(buffInfos);
skillInfos.SetupRef_item_Id(skill_effectInfos);
user_character2Infos.SetupRef_item_Id(user_characterInfos);

        }
        
    }
}
