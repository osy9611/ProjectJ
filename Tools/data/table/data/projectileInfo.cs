using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
namespace DesignTable
{
    [ProtoContract]
    public class projectileInfo
    {
        [ProtoMember(1)] 
        public int projectile_Id;
        [ProtoMember(2)] 
        public string projectile_path;
        [ProtoMember(3)] 
        public short projectile_speed;

        public projectileInfo()
        {
        }

        public projectileInfo(int projectile_Id,string projectile_path,short projectile_speed)
        {
            this.projectile_Id = projectile_Id;
            this.projectile_path = projectile_path;
            this.projectile_speed = projectile_speed;

        }
    }
      [ProtoContract]
    public class projectileInfos
    {
        [ProtoMember(1)]
        public List<projectileInfo> dataInfo = new List<projectileInfo>();
        public Dictionary<ArraySegment<byte>, projectileInfo> datas = new Dictionary<ArraySegment<byte>, projectileInfo>(new DataComparer());
        

        public bool Insert(int projectile_Id,string projectile_path,short projectile_speed)
        { 
            foreach(projectileInfo info in dataInfo)
            {
                if(info.projectile_Id == projectile_Id )
                {
                    return false;
                }
            }

            dataInfo.Add(new projectileInfo(projectile_Id,projectile_path,projectile_speed));
            return true;
        }

        public void Initialize() 
        {
            foreach(var data in dataInfo)
            {
                ArraySegment<byte> bytes = GetIdRule(data.projectile_Id);
                if (datas.ContainsKey(bytes))
                    continue;
                datas.Add(bytes,new projectileInfo(data.projectile_Id,data.projectile_path,data.projectile_speed));

                
            }
        }

        public projectileInfo Get(int projectile_Id)
        {
            projectileInfo value = null;
            
            if(datas.TryGetValue(GetIdRule(projectile_Id),out value))
                return value;
            
            return null;
        }

        

        public ArraySegment<byte> GetIdRule(int projectile_Id)
        {
            ushort total = 0;
            ushort count = 0;
                      total += sizeof(int);


            if (total == 0)
               return null;

            byte[] bytes = new byte[total];
                      Array.Copy(BitConverter.GetBytes(projectile_Id), 0, bytes, count, sizeof(int));
            count += sizeof(int);            

            
            return bytes;
        }
        
        

        

    }

}