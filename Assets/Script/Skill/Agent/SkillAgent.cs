using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SkillAgent
{
    protected BaseActor actor;
    private EventEmmiter skillEvent;
    private BuffManager buffManager;
    private ActionManager actionManager;
    public ActionManager ActionManager { get => actionManager; }

    public virtual void Init(BaseActor actor)
    {
        this.actor = actor;
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
        if (actionManager.CheckSkillType(skillId) == null)
            return;

        actionManager.RegisterSkill(skillId);
        if (actionManager.CheckSkillType(skillId) == DesignEnum.SkillType.Normal)
            actor.FSM.ChangeState(Define.ObjectState.Attack);
        else
            actor.FSM.ChangeState(Define.ObjectState.Skill);
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
