using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{
    [ProtoContract]
    public class user_character2Info
    {
        [ProtoMember(1)] 
        public int char_Id;
        [ProtoMember(2)] 
        public int char_classId;
          public user_characterInfo char_classId_ref;        
        [ProtoMember(3)] 
        public sbyte char_Gender;
        [ProtoMember(4)] 
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
      [ProtoContract]
    public class user_character2Infos
    {
        [ProtoMember(1)]
        public List<user_character2Info> dataInfo = new List<user_character2Info>();
        public Dictionary<ArraySegment<byte>, user_character2Info> datas = new Dictionary<ArraySegment<byte>, user_character2Info>(new DataComparer());
        

        public bool Insert(int char_Id,int char_classId,sbyte char_Gender,sbyte char_modelID)
        { 
            foreach(user_character2Info info in dataInfo)
            {
                if(info.char_Id == char_Id )
                {
                    return false;
                }
            }

            dataInfo.Add(new user_character2Info(char_Id,char_classId,char_Gender,char_modelID));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.char_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new user_character2Info(data.char_Id,data.char_classId,data.char_Gender,data.char_modelID));

                
            }
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
        
        

        public void SetupRef_item_Id(user_characterInfos infos)
        {
            foreach(user_character2Info data in dataInfo)
            {
                if(data.char_classId != -1)
                {
                    data.char_classId_ref = infos.Get((int)data.char_classId);
                }
            }
        }


    }

}
