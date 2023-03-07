using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{
    [ProtoContract]
    public class monster_deployInfo
    {
        [ProtoMember(1)] 
        public short mon_fieldType;
        [ProtoMember(2)] 
        public short mon_id;
        [ProtoMember(3)] 
        public short mon_type;

        public monster_deployInfo()
        {
        }

        public monster_deployInfo(short mon_fieldType,short mon_id,short mon_type)
        {
            this.mon_fieldType = mon_fieldType;
            this.mon_id = mon_id;
            this.mon_type = mon_type;

        }
    }
      [ProtoContract]
    public class monster_deployInfos
    {
        [ProtoMember(1)]
        public List<monster_deployInfo> dataInfo = new List<monster_deployInfo>();
        public Dictionary<ArraySegment<byte>, monster_deployInfo> datas = new Dictionary<ArraySegment<byte>, monster_deployInfo>(new DataComparer());
        public Dictionary<ArraySegment<byte>,List<monster_deployInfo>> listData = new Dictionary<ArraySegment<byte>, List<monster_deployInfo>>(new DataComparer());

        public bool Insert(short mon_fieldType,short mon_id,short mon_type)
        { 
            foreach(monster_deployInfo info in dataInfo)
            {
                if(info.mon_fieldType == mon_fieldType &&info.mon_id == mon_id )
                {
                    return false;
                }
            }

            dataInfo.Add(new monster_deployInfo(mon_fieldType,mon_id,mon_type));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.mon_fieldType,data.mon_id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new monster_deployInfo(data.mon_fieldType,data.mon_id,data.mon_type));

                
bytes = GetListIdRule(data.mon_fieldType);
if(listData.ContainsKey(bytes))
{
    listData[bytes].Add(data);
}
else 
{
                    
    listData.Add(bytes,new List<monster_deployInfo>());
    listData[bytes].Add(data);
}

            }
        }

        public monster_deployInfo Get(short mon_fieldType,short mon_id)
        {
            monster_deployInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(mon_fieldType,mon_id),out value))
                return value;
            
            return null;
        }

        
public List<monster_deployInfo> GetListById(short mon_fieldType)
{
    List<monster_deployInfo> value = null;
    ArraySegment<byte> bytes = GetListIdRule(mon_fieldType);
    if(listData.TryGetValue(bytes,out value))
        return value;
    return null;
}


        public ArraySegment<byte> GetIdRule(short mon_fieldType,short mon_id)
        {
            ushort total = 0;
            ushort count = 0;
                      total += sizeof(short);
          total += sizeof(short);


            if (total == 0)
               return null;

            byte[] bytes = new byte[total];
                      Array.Copy(BitConverter.GetBytes(mon_fieldType), 0, bytes, count, sizeof(short));
            count += sizeof(short);            
          Array.Copy(BitConverter.GetBytes(mon_id), 0, bytes, count, sizeof(short));
            count += sizeof(short);            

            
            return bytes;
        }
        
        
public ArraySegment<byte> GetListIdRule(short mon_fieldType)
{
    ushort total = 0;
    ushort count = 0;
              total += sizeof(short);

    if (total == 0)
        return null;
            
    byte[] bytes = new byte[total];
              Array.Copy(BitConverter.GetBytes(mon_fieldType), 0, bytes, count, sizeof(short));
            count += sizeof(short);            

    return bytes;
}


        

    }

}
