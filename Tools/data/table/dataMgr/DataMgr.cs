using System.Collections.Generic;
using System.IO;
namespace DesignTable
{
    public enum TableId
    {
        skill = 1013,
user_character = 1011,
user_character2 = 1012,

    }
    
    public class DataMgr
    {
        private delegate void LoadHandler();
        private delegate void ClearHandler();

        private Dictionary<int, DataMgr.LoadHandler> loadHandlerList = new Dictionary<int, LoadHandler>();
        private Dictionary<int, DataMgr.ClearHandler> clearHandlerList = new Dictionary<int, ClearHandler>();
        private bool isCallInit = false;
        private skillInfos skillInfos;
private user_characterInfos user_characterInfos;
private user_character2Infos user_character2Infos;
public skillInfos SkillInfos => skillInfos;
public user_characterInfos User_characterInfos => user_characterInfos;
public user_character2Infos User_character2Infos => user_character2Infos;

        
        public void Init()
        {
            if (isCallInit)
                return;

            RegisterLoadHandler();
            RegisterClearHandler();
            isCallInit = true;
        }

        public void LoadData(TableId dataType)
        {
            loadHandlerList[(int)dataType]();
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
            loadHandlerList.Add(1013, new DataMgr.LoadHandler(LoadskillInfos));
loadHandlerList.Add(1011, new DataMgr.LoadHandler(Loaduser_characterInfos));
loadHandlerList.Add(1012, new DataMgr.LoadHandler(Loaduser_character2Infos));

        }  

        private void RegisterClearHandler()
        {
            clearHandlerList.Add(1013, ClearDataskillInfos);
clearHandlerList.Add(1011, ClearDatauser_characterInfos);
clearHandlerList.Add(1012, ClearDatauser_character2Infos);

        }
        
        private void LoadskillInfos()
{
    skillInfos = new skillInfos();
}
private void Loaduser_characterInfos()
{
    user_characterInfos = new user_characterInfos();
}
private void Loaduser_character2Infos()
{
    user_character2Infos = new user_character2Infos();
}

        
        private void ClearDataskillInfos()
{
    if(skillInfos != null)
        skillInfos=null;
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
            skillInfos.SetupRef_item_Id(user_characterInfos);
skillInfos.SetupRef_item_Id(user_character2Infos);
user_character2Infos.SetupRef_item_Id(user_characterInfos);

        }
        
    }
}
