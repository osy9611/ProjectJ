using Module.Core.Systems.Collections.Generic;
using Module.Core.Systems.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    UnorderedList<BaseBuff> buffs;
    private BuffManager buffManager;
    private SkillAgent agent;

    public void Init(SkillAgent agent)
    {
        this.agent = agent;
        buffs =new UnorderedList<BaseBuff>();
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
        switch(buffId)
        {
            case 0:
                buff = new AddATK();
                break;
            case 1:
                buff = new AddDEF();
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
