using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAgent
{
    private EventEmmiter skillEvent;
    private BuffManager buffManager;
    private ActionManager actionManager;

    public virtual void Init()
    {
        skillEvent = new EventEmmiter();
        buffManager = new BuffManager();
        buffManager.Init(skillEvent);
        actionManager = new ActionManager();
        actionManager.Init(skillEvent);
    }


    public virtual void Execute() 
    {
        skillEvent.Invoke();
    }

    public virtual void ResetData() { }

    public virtual void CheckAttackRange() { }

    public virtual void OnSkill(int skillId) 
    {
        actionManager.RegisterSkill(skillId);
    }

    public virtual float GetSkillAttackRange(int skillId) { return 0; }

    public virtual void OnBuff(int buffId) 
    {
        buffManager.Register(buffId);
    }

    public virtual void RemoveBuff(int buffId) 
    { 

    }

}
