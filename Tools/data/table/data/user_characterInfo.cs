using System;
using System.Collections;
using System.Collections.Generic;
namespace DesignTable
{
    public class user_characterInfo
    {
        public int char_Id;
        public sbyte char_classId;
        public sbyte char_Gender;
        public sbyte char_model_id;
        public float char_Common_Attack_Cooltime;
        public float char_SizeX;
        public float char_SizeY;
        public float char_SizeZ;
        public float char_Move_Speed;
        public string char_Prefab;
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
  
    public class user_characterInfos
    {
        public Dictionary<ArraySegment<byte>, user_characterInfo> datas;
      
        public bool Insert(int char_Id,sbyte char_classId,sbyte char_Gender,sbyte char_model_id,float char_Common_Attack_Cooltime,float char_SizeX,float char_SizeY,float char_SizeZ,float char_Move_Speed,string char_Prefab,string char_Selection_Prefab)
        {
            ArraySegment<byte> bytes = GetIdRule(char_Id);
            if (datas.ContainsKey(bytes))
                return false;

            datas.Add(bytes,new user_characterInfo(char_Id,char_classId,char_Gender,char_model_id,char_Common_Attack_Cooltime,char_SizeX,char_SizeY,char_SizeZ,char_Move_Speed,char_Prefab,char_Selection_Prefab));
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
