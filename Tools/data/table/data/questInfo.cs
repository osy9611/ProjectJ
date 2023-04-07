using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{
    [ProtoContract]
    public class questInfo
    {
        [ProtoMember(1)] 
        public short quest_Id;
        [ProtoMember(2)] 
        public short quest_type;
        [ProtoMember(3)] 
        public string reward;
        [ProtoMember(4)] 
        public string quest_target;
        [ProtoMember(5)] 
        public string quest_description;

        public questInfo()
        {
        }

        public questInfo(short quest_Id,short quest_type,string reward,string quest_target,string quest_description)
        {
            this.quest_Id = quest_Id;
            this.quest_type = quest_type;
            this.reward = reward;
            this.quest_target = quest_target;
            this.quest_description = quest_description;

        }
    }
      [ProtoContract]
    public class questInfos
    {
        [ProtoMember(1)]
        public List<questInfo> dataInfo = new List<questInfo>();
        public Dictionary<ArraySegment<byte>, questInfo> datas = new Dictionary<ArraySegment<byte>, questInfo>(new DataComparer());
        

        public bool Insert(short quest_Id,short quest_type,string reward,string quest_target,string quest_description)
        { 
            foreach(questInfo info in dataInfo)
            {
                if(info.quest_Id == quest_Id )
                {
                    return false;
                }
            }

            dataInfo.Add(new questInfo(quest_Id,quest_type,reward,quest_target,quest_description));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.quest_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new questInfo(data.quest_Id,data.quest_type,data.reward,data.quest_target,data.quest_description));

                
            }
        }

        public questInfo Get(short quest_Id)
        {
            questInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(quest_Id),out value))
                return value;
            
            return null;
        }

        

        public ArraySegment<byte> GetIdRule(short quest_Id)
        {
            ushort total = 0;
            ushort count = 0;
                      total += sizeof(short);


            if (total == 0)
               return null;

            byte[] bytes = new byte[total];
                      Array.Copy(BitConverter.GetBytes(quest_Id), 0, bytes, count, sizeof(short));
            count += sizeof(short);            

            
            return bytes;
        }
        
        

        

    }

}
