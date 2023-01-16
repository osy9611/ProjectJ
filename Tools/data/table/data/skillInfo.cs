using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{
    [ProtoContract]
    public class skillInfo
    {
        [ProtoMember(1)] 
        public int skill_Id;
        [ProtoMember(2)] 
        public int char_classId;
          public user_characterInfo char_classId_ref;        
        [ProtoMember(3)] 
        public sbyte char_Gender;
          public user_character2Info char_Gender_ref;        
        [ProtoMember(4)] 
        public sbyte char_modelID;

        public skillInfo()
        {
        }

        public skillInfo(int skill_Id,int char_classId,sbyte char_Gender,sbyte char_modelID)
        {
            this.skill_Id = skill_Id;
            this.char_classId = char_classId;
            this.char_Gender = char_Gender;
            this.char_modelID = char_modelID;

        }
    }
      [ProtoContract]
    public class skillInfos
    {
        [ProtoMember(1)]
        public List<skillInfo> m_data = new List<skillInfo>();
        public Dictionary<ArraySegment<byte>, skillInfo> datas = new Dictionary<ArraySegment<byte>, skillInfo>();
      
        public bool Insert(int skill_Id,int char_classId,sbyte char_Gender,sbyte char_modelID)
        {
            ArraySegment<byte> bytes = GetIdRule(skill_Id);
            if (datas.ContainsKey(bytes))
                return false;

            datas.Add(bytes,new skillInfo(skill_Id,char_classId,char_Gender,char_modelID));
            m_data.Add(new skillInfo(skill_Id,char_classId,char_Gender,char_modelID));
            return true;
        }

        public skillInfo Get(int skill_Id)
        {
            skillInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(skill_Id),out value))
                return value;
            
            return null;
        }

        public ArraySegment<byte> GetIdRule(int skill_Id)
        {
            ushort count = 0;
                      count += sizeof(int);


            if (count == 0)
               return null;

            byte[] bytes = new byte[count];
            return bytes;
        }

        public void SetupRef_item_Id(user_characterInfos infos)
        {
            foreach(skillInfo data in datas.Values)
            {
                if(data.char_classId != -1)
                {
                    data.char_classId_ref = infos.Get((int)data.char_classId);
                }
            }
        }
public void SetupRef_item_Id(user_character2Infos infos)
        {
            foreach(skillInfo data in datas.Values)
            {
                if(data.char_Gender != -1)
                {
                    data.char_Gender_ref = infos.Get((sbyte)data.char_Gender);
                }
            }
        }


    }

}
