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
        public Dictionary<ArraySegment<byte>, skillInfo> datas = new Dictionary<ArraySegment<byte>, skillInfo>(new DataComparer());
        public Dictionary<ArraySegment<byte>,List<skillInfo>> listData = new Dictionary<ArraySegment<byte>, List<skillInfo>>(new DataComparer());

        public bool Insert(int unit_Class,int skill_Id,float skill_coolTime,float skill_range,int skill_buffId)
        { 
            foreach(skillInfo info in dataInfo)
            {
                if(info.unit_Class == unit_Class &&info.skill_Id == skill_Id )
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
                ArraySegment<byte> bytes = GetIdRule(data.unit_Class,data.skill_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new skillInfo(data.unit_Class,data.skill_Id,data.skill_coolTime,data.skill_range,data.skill_buffId));

                
bytes = GetListIdRule(data.unit_Class);
if(listData.ContainsKey(bytes))
{
    listData[bytes].Add(data);
}
else 
{
                    
    listData.Add(bytes,new List<skillInfo>());
    listData[bytes].Add(data);
}

            }
        }

        public skillInfo Get(int unit_Class,int skill_Id)
        {
            skillInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(unit_Class,skill_Id),out value))
                return value;
            
            return null;
        }

        
public List<skillInfo> GetListById(int unit_Class)
{
    List<skillInfo> value = null;
    ArraySegment<byte> bytes = GetListIdRule(unit_Class);
    if(listData.TryGetValue(bytes,out value))
        return value;
    return null;
}


        public ArraySegment<byte> GetIdRule(int unit_Class,int skill_Id)
        {
            ushort total = 0;
            ushort count = 0;
                      total += sizeof(int);
          total += sizeof(int);


            if (total == 0)
               return null;

            byte[] bytes = new byte[total];
                      Array.Copy(BitConverter.GetBytes(unit_Class), 0, bytes, count, sizeof(int));
            count += sizeof(int);            
          Array.Copy(BitConverter.GetBytes(skill_Id), 0, bytes, count, sizeof(int));
            count += sizeof(int);            

            
            return bytes;
        }
        
        
public ArraySegment<byte> GetListIdRule(int unit_Class)
{
    ushort total = 0;
    ushort count = 0;
              total += sizeof(int);

    if (total == 0)
        return null;
            
    byte[] bytes = new byte[total];
              Array.Copy(BitConverter.GetBytes(unit_Class), 0, bytes, count, sizeof(int));
            count += sizeof(int);            

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
