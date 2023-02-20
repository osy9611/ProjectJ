using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatusDefine;

public class StatusAgent
{
    protected BaseActor actor;
    protected Status<long>[] status;

    protected long[] hp;

    public void Init(BaseActor actor)
    {
        this.actor = actor;

        status = new Status<long>[StatusDefine.StatusGroupCount];
        for (int i = 0; i < StatusDefine.StatusGroupCount; ++i)
        {
            status[i] = new Status<long>(StatusDefine.StatusCount);
        }

        hp = new long[StatusDefine.HpTypeCount];
    }

    public void IncreaseStatus(StatusDefine.StatusGroupId statusGroupID, DesignEnum.AttributeId attributeId, long value = 0, bool calcPercent = false)
    {
        long result = value;
        if (calcPercent)
        {
            result = StatusDefine.FloatToLong(status[(int)StatusDefine.StatusGroupId.Default][(int)attributeId] * StatusDefine.IntToPercent((int)value));
        }
        status[(int)statusGroupID][(int)attributeId] += result;
    }

    public void DecreaseStatus(StatusDefine.StatusGroupId statusGroupID, DesignEnum.AttributeId attributeId, long value = 0, bool calcPercent = false)
    {
        long result = value;
        if (calcPercent)
        {
            result = StatusDefine.FloatToLong(status[(int)StatusDefine.StatusGroupId.Default][(int)attributeId] * StatusDefine.IntToPercent((int)value));
        }
        status[(int)statusGroupID][(int)attributeId] -= result;
    }

    public void IncreaseHP(StatusDefine.HPType hpType, long value)
    {
        long result = value;

        if (hpType == StatusDefine.HPType.NowHP)
        {

            if (hp[(int)StatusDefine.HPType.NowHP] + result >= hp[(int)StatusDefine.HPType.MaxHP])
            {
                result = hp[(int)StatusDefine.HPType.MaxHP];
            }

            hp[(int)StatusDefine.HPType.NowHP] += result;
        }
        else
        {
            hp[(int)StatusDefine.HPType.MaxHP] += result;
        }
    }

    public void DecreaseHP(StatusDefine.HPType hpType, long value)
    {
        long result = value;

        if (hpType == StatusDefine.HPType.NowHP)
        {

            if (hp[(int)StatusDefine.HPType.NowHP] - result <= 0)
            {
                result = 0;
                actor.FSM.ChangeState(Define.ObjectState.Death);
            }

            hp[(int)StatusDefine.HPType.NowHP] -= result;
        }
        else
        {
            hp[(int)StatusDefine.HPType.MaxHP] -= result;
        }
    }

}
