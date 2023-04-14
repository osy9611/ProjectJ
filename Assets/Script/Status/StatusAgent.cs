using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static StatusDefine;

public class StatusAgent
{
    protected BaseActor actor;
    protected Status<long>[] status;
    protected Status<long> totalStatus;
    public Status<long> TotalStatus
    {
        get
        {
            CalcTotalStatus();
            return totalStatus;
        }
    }

    protected long[] hp;
    public float HpPer { get => (float)hp[(int)StatusDefine.HPType.NowHP] / (float)hp[(int)StatusDefine.HPType.MaxHP]; }

    public void Init(BaseActor actor)
    {
        this.actor = actor;

        status = new Status<long>[StatusDefine.StatusGroupCount];
        for (int i = 0; i < StatusDefine.StatusGroupCount; ++i)
        {
            status[i] = new Status<long>(StatusDefine.StatusCount);
        }

        totalStatus = new Status<long>(StatusDefine.StatusCount);

        hp = new long[StatusDefine.HpTypeCount];
    }

    public void SetStatus(long atk, long def, long hp)
    {
        int defaultEnum = (int)StatusDefine.StatusGroupId.Default;

        status[defaultEnum][(int)DesignEnum.AttributeId.Atk] = atk;
        status[defaultEnum][(int)DesignEnum.AttributeId.Def] = def;

        this.hp[(int)StatusDefine.HPType.NowHP] = hp;
        this.hp[(int)StatusDefine.HPType.MaxHP] = hp;

    }

    public void CalcTotalStatus()
    {
        totalStatus[(int)DesignEnum.AttributeId.Atk] = 0;
        totalStatus[(int)DesignEnum.AttributeId.Def] = 0;

        foreach (var groupStatus in status)
        {
            totalStatus[(int)DesignEnum.AttributeId.Atk] += groupStatus[(int)DesignEnum.AttributeId.Atk];
            totalStatus[(int)DesignEnum.AttributeId.Def] += groupStatus[(int)DesignEnum.AttributeId.Def];
        }
    }

    public void IncreaseStatus(StatusDefine.StatusGroupId statusGroupID, DesignEnum.AttributeId attributeId, long value = 0, bool calcPercent = false)
    {
        long result = value;

        Debug.Log(result);
        if (calcPercent)
        {
            result = StatusDefine.FloatToLong(status[(int)StatusDefine.StatusGroupId.Default][(int)attributeId] * StatusDefine.IntToPercent((int)value));
        }
        status[(int)statusGroupID][(int)attributeId] += result;
        Debug.Log(status[(int)statusGroupID][(int)attributeId]);
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

    public void CalcDecreaseHP(StatusDefine.HPType hpType, BaseActor checkActor)
    {
        CalcTotalStatus();

        long damage = checkActor.StatusAgent.TotalStatus[(int)DesignEnum.AttributeId.Atk] - totalStatus[(int)DesignEnum.AttributeId.Def];

        actor.Creature.HudUnitInfo.SetDamage(damage);
        if (damage > 0)
        {
            DecreaseHP(hpType, damage);
        }
    }

    public void DecreaseHP(StatusDefine.HPType hpType, long value)
    {
        long result = value;


        if (hpType == StatusDefine.HPType.NowHP)
        {

            if (hp[(int)StatusDefine.HPType.NowHP] - result <= 0)
            {

                hp[(int)StatusDefine.HPType.NowHP] = 0;
                actor.FSM.ChangeState(Define.ObjectState.Death);
                
                if (actor.UnitType == DesignEnum.UnitType.Monster)
                {
                    short modelId = (actor as MonsterActor).ModelID;
                    Managers.Quest.CheckQuest((int)DesignEnum.QuestType.MonsterHunt, modelId.ToString());
                }
            }

            hp[(int)StatusDefine.HPType.NowHP] -= result;
        }
        else
        {
            hp[(int)StatusDefine.HPType.MaxHP] -= result;
        }
    }

    public void ResetHP()
    {
        hp[(int)StatusDefine.HPType.NowHP] = hp[(int)StatusDefine.HPType.MaxHP];
    }

}
