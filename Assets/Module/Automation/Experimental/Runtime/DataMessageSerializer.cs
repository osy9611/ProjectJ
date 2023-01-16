using DesignTable;
using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
public sealed class DataMessageSerializer
{
    public void Write(user_characterInfos info)
    {
        System.IO.MemoryStream reqStream = new System.IO.MemoryStream();
        Serializer.Serialize(reqStream, info);
        Debug.Log(info.m_data.Count);
        byte[] buffer = reqStream.ToArray();
        //Read(buffer);
        System.IO.MemoryStream resStream = new System.IO.MemoryStream(buffer);
        user_characterInfos res = ProtoBuf.Serializer.Deserialize<user_characterInfos>(resStream);
        Debug.Log(res.m_data[0].char_Selection_Prefab);
    }

    public void Read(byte[] buffer)
    {
        System.IO.MemoryStream resStream = new System.IO.MemoryStream(buffer);
        user_characterInfos res= ProtoBuf.Serializer.Deserialize<user_characterInfos>(resStream);
    }
}
