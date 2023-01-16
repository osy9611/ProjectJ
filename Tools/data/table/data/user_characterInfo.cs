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
        public sbyte char_Gender;
        [ProtoMember(4)] 
        public sbyte char_model_id;
        [ProtoMember(5)] 
        public float char_Common_Attack_Cooltime;
        [ProtoMember(6)] 
        public float char_SizeX;
        [ProtoMember(7)] 
        public float char_SizeY;
        [ProtoMember(8)] 
        public float char_SizeZ;
        [ProtoMember(9)] 
        public float char_Move_Speed;
        [ProtoMember(10)] 
        public string char_Prefab;
        [ProtoMember(11)] 
        public string char_Selection_Prefab;

        public user_characterInfo()
        {
        }

        public user_characterInfo(int char_Id,sbyte char_classId,sbyte char_Gender,sbyte char_model_id,float char_Common_Attack_Cooltime,float char_SizeX,float char_SizeY,float char_SizeZ,float char_Move_Speed,string char_Prefab,string char_Selection_Prefab)
        {
            this.char_Id = char_Id;
            this.char_classId = char_classId;
            this.char_Gender = char_Gender;
            this.char_model_id = char_model_id;
            this.char_Common_Attack_Cooltime = char_Common_Attack_Cooltime;
            this.char_SizeX = char_SizeX;
            this.char_SizeY = char_SizeY;
            this.char_SizeZ = char_SizeZ;
            this.char_Move_Speed = char_Move_Speed;
            this.char_Prefab = char_Prefab;
            this.char_Selection_Prefab = char_Selection_Prefab;

        }
    }
      [ProtoContract]
    public class user_characterInfos
    {
        [ProtoMember(1)]
        public List<user_characterInfo> m_data = new List<user_characterInfo>();
        public Dictionary<ArraySegment<byte>, user_characterInfo> datas = new Dictionary<ArraySegment<byte>, user_characterInfo>();
      
        public bool Insert(int char_Id,sbyte char_classId,sbyte char_Gender,sbyte char_model_id,float char_Common_Attack_Cooltime,float char_SizeX,float char_SizeY,float char_SizeZ,float char_Move_Speed,string char_Prefab,string char_Selection_Prefab)
        {
            ArraySegment<byte> bytes = GetIdRule(char_Id);
            if (datas.ContainsKey(bytes))
                return false;

            datas.Add(bytes,new user_characterInfo(char_Id,char_classId,char_Gender,char_model_id,char_Common_Attack_Cooltime,char_SizeX,char_SizeY,char_SizeZ,char_Move_Speed,char_Prefab,char_Selection_Prefab));
            m_data.Add(new user_characterInfo(char_Id,char_classId,char_Gender,char_model_id,char_Common_Attack_Cooltime,char_SizeX,char_SizeY,char_SizeZ,char_Move_Speed,char_Prefab,char_Selection_Prefab));
            return true;
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
            ushort count = 0;
                      count += sizeof(int);


            if (count == 0)
               return null;

            byte[] bytes = new byte[count];
            return bytes;
        }

        

    }

}
