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
        private List<user_characterInfo> dataInfo = new List<user_characterInfo>();
        public Dictionary<ArraySegment<byte>, user_characterInfo> datas = new Dictionary<ArraySegment<byte>, user_characterInfo>(new DataComparer());
        

        public bool Insert(int char_Id,sbyte char_classId,sbyte char_Gender,sbyte char_model_id,float char_Common_Attack_Cooltime,float char_SizeX,float char_SizeY,float char_SizeZ,float char_Move_Speed,string char_Prefab,string char_Selection_Prefab)
        { 
            foreach(user_characterInfo info in dataInfo)
            {
                if(info.char_Id == char_Id )
                {
                    return false;
                }
            }

            dataInfo.Add(new user_characterInfo(char_Id,char_classId,char_Gender,char_model_id,char_Common_Attack_Cooltime,char_SizeX,char_SizeY,char_SizeZ,char_Move_Speed,char_Prefab,char_Selection_Prefab));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.char_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new user_characterInfo(data.char_Id,data.char_classId,data.char_Gender,data.char_model_id,data.char_Common_Attack_Cooltime,data.char_SizeX,data.char_SizeY,data.char_SizeZ,data.char_Move_Speed,data.char_Prefab,data.char_Selection_Prefab));

                
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
