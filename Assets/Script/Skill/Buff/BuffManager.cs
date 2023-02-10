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
    private BuffManager buffManager;
    private SkillAgent agent;

    public void Init(SkillAgent agent)
    {
        this.agent = agent;
        buffs =new UnorderedList<BaseBuff>();

        List<skillInfo> skillInfo = Managers.Data.SkillInfos.GetListById((int)DesignEnum.ClassType.Monk);

    }

    public void Excute() 
    {
        if (buffs.Count == 0)
            return;

        for(int i=0,range=buffs.Count; i<range; ++i)
        {
            if (buffs[i].IsEnd)
            {
                buffs.Remove(buffs[i]);
            }
            else
            {
                buffs[i].Execute();
            }
        }
    }

    public void Add(int buffId)
    {
        BaseBuff buff = null;
        switch((DesignEnum.BuffType)buffId)
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
                buff= new Dot();
                break;
        }

        if (buff == null)
            return;

        buff.Init(new EventArgs<float, float>(3, 0),this);

        buffs.Add(buff);
    }


    public void Remove(BaseBuff baseBuff)
    {
        buffs.Remove(baseBuff);
    }
}
