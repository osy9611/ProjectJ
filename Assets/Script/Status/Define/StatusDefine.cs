using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDefine 
{
    static public int StatusCount = System.Enum.GetNames(typeof(DesignEnum.AttributeId)).Length;
    static public int StatusGroupCount = System.Enum.GetNames(typeof(StatusGroupId)).Length;
    static public int HpTypeCount = System.Enum.GetNames(typeof(HPType)).Length;
    public enum HPType
    {
        NowHP,
        MaxHP,
    }

    public enum StatusGroupId
    {
        Default,
        Equip,
        Buff,
        Passive
    }

    public static float IntToFloat(int value) => value * 0.0001f;
    public static float FloatToInt(float value) => (int)(value * 10000);
    public static float IntToPercent(int value) => value * 0.01f;
    public static float FloatToPercent(float value) => value * 100;
    public static float LongToFloat(long value) => value * 0.0001f;
    public static long FloatToLong(float value) => (long)(value * 10000);
    public static float LongToPercent(long value) => value * 0.01f;
}
