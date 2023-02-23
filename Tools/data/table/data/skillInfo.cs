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
        public int unit_type;
        [ProtoMember(3)] 
        public int skill_Id;
        [ProtoMember(4)] 
        public float skill_coolTime;
        [ProtoMember(5)] 
        public float skill_range;
        [ProtoMember(6)] 
        public float skill_radius;
        [ProtoMember(7)] 
        public float skill_scale;
        [ProtoMember(8)] 
        public int skill_buffId;
          public buffInfo skill_buffId_ref;        
        [ProtoMember(9)] 
        public sbyte skill_type;
        [ProtoMember(10)] 
        public sbyte skill_attackType;
        [ProtoMember(11)] 
        public bool skill_contoroll;
        [ProtoMember(12)] 
        public bool skill_dash;
        [ProtoMember(13)] 
        public float skill_dashSpeed;
        [ProtoMember(14)] 
        public bool skill_judgeAni;
        [ProtoMember(15)] 
        public float skill_judgeTime;
        [ProtoMember(16)] 
        public int effect_Id;
          public skill_effectInfo effect_Id_ref;        

        public skillInfo()
        {
        }

        public skillInfo(int unit_Class,int unit_type,int skill_Id,float skill_coolTime,float skill_range,float skill_radius,float skill_scale,int skill_buffId,sbyte skill_type,sbyte skill_attackType,bool skill_contoroll,bool skill_dash,float skill_dashSpeed,bool skill_judgeAni,float skill_judgeTime,int effect_Id)
        {
            this.unit_Class = unit_Class;
            this.unit_type = unit_type;
            this.skill_Id = skill_Id;
            this.skill_coolTime = skill_coolTime;
            this.skill_range = skill_range;
            this.skill_radius = skill_radius;
            this.skill_scale = skill_scale;
            this.skill_buffId = skill_buffId;
            this.skill_type = skill_type;
            this.skill_attackType = skill_attackType;
            this.skill_contoroll = skill_contoroll;
            this.skill_dash = skill_dash;
            this.skill_dashSpeed = skill_dashSpeed;
            this.skill_judgeAni = skill_judgeAni;
            this.skill_judgeTime = skill_judgeTime;
            this.effect_Id = effect_Id;

        }
    }
      [ProtoContract]
    public class skillInfos
    {
        [ProtoMember(1)]
        public List<skillInfo> dataInfo = new List<skillInfo>();
        public Dictionary<ArraySegment<byte>, skillInfo> datas = new Dictionary<ArraySegment<byte>, skillInfo>(new DataComparer());
        public Dictionary<ArraySegment<byte>,List<skillInfo>> listData = new Dictionary<ArraySegment<byte>, List<skillInfo>>(new DataComparer());

        public bool Insert(int unit_Class,int unit_type,int skill_Id,float skill_coolTime,float skill_range,float skill_radius,float skill_scale,int skill_buffId,sbyte skill_type,sbyte skill_attackType,bool skill_contoroll,bool skill_dash,float skill_dashSpeed,bool skill_judgeAni,float skill_judgeTime,int effect_Id)
        { 
            foreach(skillInfo info in dataInfo)
            {
                if(info.unit_Class == unit_Class &&info.unit_type == unit_type &&info.skill_Id == skill_Id )
                {
                    return false;
                }
            }

            dataInfo.Add(new skillInfo(unit_Class,unit_type,skill_Id,skill_coolTime,skill_range,skill_radius,skill_scale,skill_buffId,skill_type,skill_attackType,skill_contoroll,skill_dash,skill_dashSpeed,skill_judgeAni,skill_judgeTime,effect_Id));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.unit_Class,data.unit_type,data.skill_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new skillInfo(data.unit_Class,data.unit_type,data.skill_Id,data.skill_coolTime,data.skill_range,data.skill_radius,data.skill_scale,data.skill_buffId,data.skill_type,data.skill_attackType,data.skill_contoroll,data.skill_dash,data.skill_dashSpeed,data.skill_judgeAni,data.skill_judgeTime,data.effect_Id));

                
bytes = GetListIdRule(data.unit_Class,data.unit_type);
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

        public skillInfo Get(int unit_Class,int unit_type,int skill_Id)
        {
            skillInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(unit_Class,unit_type,skill_Id),out value))
                return value;
            
            return null;
        }

        
public List<skillInfo> GetListById(int unit_Class,int unit_type)
{
    List<skillInfo> value = null;
    ArraySegment<byte> bytes = GetListIdRule(unit_Class,unit_type);
    if(listData.TryGetValue(bytes,out value))
        return value;
    return null;
}


        public ArraySegment<byte> GetIdRule(int unit_Class,int unit_type,int skill_Id)
        {
            ushort total = 0;
            ushort count = 0;
                      total += sizeof(int);
          total += sizeof(int);
          total += sizeof(int);


            if (total == 0)
               return null;

            byte[] bytes = new byte[total];
                      Array.Copy(BitConverter.GetBytes(unit_Class), 0, bytes, count, sizeof(int));
            count += sizeof(int);            
          Array.Copy(BitConverter.GetBytes(unit_type), 0, bytes, count, sizeof(int));
            count += sizeof(int);            
          Array.Copy(BitConverter.GetBytes(skill_Id), 0, bytes, count, sizeof(int));
            count += sizeof(int);            

            
            return bytes;
        }
        
        
public ArraySegment<byte> GetListIdRule(int unit_Class,int unit_type)
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
          Array.Copy(BitConverter.GetBytes(unit_type), 0, bytes, count, sizeof(int));
            count += sizeof(int);            

    return bytes;
}


        public void SetupRef_item_Id(buffInfos infos)
        {
            foreach(skillInfo data in dataInfo)
            {
                if(data.skill_buffId != -1)
                {
                    data.skill_buffId_ref = infos.Get((int)data.skill_buffId);
                }
            }
        }
public void SetupRef_item_Id(skill_effectInfos infos)
        {
            foreach(skillInfo data in dataInfo)
            {
                if(data.effect_Id != -1)
                {
                    data.effect_Id_ref = infos.Get((int)data.effect_Id);
                }
            }
        }


    }

}
