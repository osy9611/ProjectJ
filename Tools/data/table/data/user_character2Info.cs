using System;
using System.Collections;
using System.Collections.Generic;
namespace DesignTable
{
    public class user_character2Info
    {
        public int char_Id;
        public int char_classId;
        public user_characterInfo char_classId_ref;
        public sbyte char_Gender;
        public sbyte char_modelID;

        public user_character2Info()
        {
        }

        public user_character2Info(int char_Id,int char_classId,sbyte char_Gender,sbyte char_modelID)
        {
            this.char_Id = char_Id;
            this.char_classId = char_classId;
            this.char_Gender = char_Gender;
            this.char_modelID = char_modelID;

        }
    }
  
    public class user_character2Infos
    {
        public Dictionary<ArraySegment<byte>, user_character2Info> datas;
      
        public bool Insert(int char_Id,int char_classId,sbyte char_Gender,sbyte char_modelID)
        {
            ArraySegment<byte> bytes = GetIdRule(char_Id);
            if (datas.ContainsKey(bytes))
                return false;

            datas.Add(bytes,new user_character2Info(char_Id,char_classId,char_Gender,char_modelID));
            return true;
        }

        public user_character2Info Get(int char_Id)
        {
            user_character2Info value = null;
            
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

        public void SetupRef_item_Id(user_characterInfos infos)
        {
            foreach(user_character2Info data in datas.Values)
            {
                if(data.char_classId != -1)
                {
                    data.char_classId_ref = infos.Get((int)data.char_classId);
                }
            }
        }


    }

}
