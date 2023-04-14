using Module.Core.Systems.Collections.Generic;
using Module.Core.Systems.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignTable;
public class BuffManager
{
    UnorderedList<BaseBuff> buffs;
    private BaseActor actor;

    public UnorderedList<BaseBuff> Buffs { get => buffs; }

    public void Init(BaseActor actor)
    {
        this.actor = actor;
        buffs = new UnorderedList<BaseBuff>();
    }

    public void Register(int buffId)
    {
        BaseBuff buff = null;
        buffInfo info = Managers.Data.BuffInfos.Get(buffId);

        switch ((DesignEnum.BuffType)info.buff_type)
        {
            case DesignEnum.BuffType.AddATK:
                buff = new AddATK();
                break;
            case DesignEnum.BuffType.AddDEF:
                buff = new AddDEF();
                break;
            case DesignEnum.BuffType.LowATK:
                buff = new LowATK();
                break;
            case DesignEnum.BuffType.LowDEF:
                buff = new LowDEF();
                break;
            case DesignEnum.BuffType.Strun:
                buff = new Strun();
                break;
            case DesignEnum.BuffType.Dot:
                buff = new Dot();
                break;
        }

        if (buff == null)
            return;

        buff.Init(this, new Args<BaseActor,buffInfo>(actor,info));
        buffs.Add(buff);
        actor.EventEmmiter.AddListener(buff.Execute);
    }


    public void UnRegister(BaseBuff baseBuff)
    {
        actor.EventEmmiter.RemoveListener(baseBuff.Execute);
        buffs.Remove(baseBuff);
    }
}
