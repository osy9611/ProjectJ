using DesignTable;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public sealed class DataMessageSerializer
{
    public byte[] Serialize(object tableInfos)
    {
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        Serializer.Serialize(stream, tableInfos);
        byte[] buffer = stream.ToArray();

        return buffer;
    }

    public object Deserialize(int tableId, byte[] buffer)
    {
        System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
        switch (tableId)
        {
            case 1014:
    return ProtoBuf.Serializer.Deserialize<buffInfos>(stream);case 1013:
    return ProtoBuf.Serializer.Deserialize<skillInfos>(stream);case 1015:
    return ProtoBuf.Serializer.Deserialize<skill_effectInfos>(stream);case 1011:
    return ProtoBuf.Serializer.Deserialize<user_characterInfos>(stream);case 1012:
    return ProtoBuf.Serializer.Deserialize<user_character2Infos>(stream);
        }

        return null;
    }
}
