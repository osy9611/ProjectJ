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
        public int unit_Class;
        [ProtoMember(2)] 
        public int skill_Id;
        [ProtoMember(3)] 
        public float skill_coolTime;
        [ProtoMember(4)] 
        public float skill_range;
        [ProtoMember(5)] 
        public int skill_buffId;
          public buffInfo skill_buffId_ref;        

        public skillInfo()
        {
        }

        public skillInfo(int unit_Class,int skill_Id,float skill_coolTime,float skill_range,int skill_buffId)
        {
            this.unit_Class = unit_Class;
            this.skill_Id = skill_Id;
            this.skill_coolTime = skill_coolTime;
            this.skill_range = skill_range;
            this.skill_buffId = skill_buffId;

        }
    }
      [ProtoContract]
    public class skillInfos
    {
        [ProtoMember(1)]
        private List<skillInfo> dataInfo = new List<skillInfo>();
        public Dictionary<ArraySegment<byte>, skillInfo> datas = new Dictionary<ArraySegment<byte>, skillInfo>();
      
        public bool Insert(int unit_Class,int skill_Id,float skill_coolTime,float skill_range,int skill_buffId)
        { 
            foreach(skillInfo info in dataInfo)
            {
                if(info.unit_Class == unit_Class )
                {
                    return false;
                }
            }

            dataInfo.Add(new skillInfo(unit_Class,skill_Id,skill_coolTime,skill_range,skill_buffId));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.unit_Class);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new skillInfo(data.unit_Class,data.skill_Id,data.skill_coolTime,data.skill_range,data.skill_buffId));
            }
        }

        public skillInfo Get(int unit_Class)
        {
            skillInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(unit_Class),out value))
                return value;
            
            return null;
        }

        public ArraySegment<byte> GetIdRule(int unit_Class)
        {
            ushort count = 0;
                      count += sizeof(int);


            if (count == 0)
               return null;

            byte[] bytes = new byte[count];
            return bytes;
        }

        public void SetupRef_item_Id(buffInfos infos)
        {
            foreach(skillInfo data in datas.Values)
            {
                if(data.skill_buffId != -1)
                {
                    data.skill_buffId_ref = infos.Get((int)data.skill_buffId);
                }
            }
        }


    }

}
