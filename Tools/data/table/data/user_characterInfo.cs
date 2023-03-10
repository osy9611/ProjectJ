using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{
    [ProtoContract]
    public class user_characterInfo
    {
        [ProtoMember(1)] 
        public int char_Id;
        [ProtoMember(2)] 
        public sbyte char_classId;
        [ProtoMember(3)] 
        public sbyte char_gender;
        [ProtoMember(4)] 
        public short char_atk;
        [ProtoMember(5)] 
        public short char_def;
        [ProtoMember(6)] 
        public short char_hp;
        [ProtoMember(7)] 
        public float char_moveSpeed;
        [ProtoMember(8)] 
        public string char_prefab;

        public user_characterInfo()
        {
        }

        public user_characterInfo(int char_Id,sbyte char_classId,sbyte char_gender,short char_atk,short char_def,short char_hp,float char_moveSpeed,string char_prefab)
        {
            this.char_Id = char_Id;
            this.char_classId = char_classId;
            this.char_gender = char_gender;
            this.char_atk = char_atk;
            this.char_def = char_def;
            this.char_hp = char_hp;
            this.char_moveSpeed = char_moveSpeed;
            this.char_prefab = char_prefab;

        }
    }
      [ProtoContract]
    public class user_characterInfos
    {
        [ProtoMember(1)]
        public List<user_characterInfo> dataInfo = new List<user_characterInfo>();
        public Dictionary<ArraySegment<byte>, user_characterInfo> datas = new Dictionary<ArraySegment<byte>, user_characterInfo>(new DataComparer());
        

        public bool Insert(int char_Id,sbyte char_classId,sbyte char_gender,short char_atk,short char_def,short char_hp,float char_moveSpeed,string char_prefab)
        { 
            foreach(user_characterInfo info in dataInfo)
            {
                if(info.char_Id == char_Id )
                {
                    return false;
                }
            }

            dataInfo.Add(new user_characterInfo(char_Id,char_classId,char_gender,char_atk,char_def,char_hp,char_moveSpeed,char_prefab));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.char_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new user_characterInfo(data.char_Id,data.char_classId,data.char_gender,data.char_atk,data.char_def,data.char_hp,data.char_moveSpeed,data.char_prefab));

                
            }
        }

        public user_characterInfo Get(int char_Id)
        {
            user_characterInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(char_Id),out value))
                return value;
            
            return null;
        }

        

        public ArraySegment<byte> GetIdRule(int char_Id)
        {
            ushort total = 0;
            ushort count = 0;
                      total += sizeof(int);


            if (total == 0)
               return null;

            byte[] bytes = new byte[total];
                      Array.Copy(BitConverter.GetBytes(char_Id), 0, bytes, count, sizeof(int));
            count += sizeof(int);            

            
            return bytes;
        }
        
        

        

    }

}
