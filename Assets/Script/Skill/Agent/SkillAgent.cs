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
    private EffectInfo effectInfo;
    private ActionManager actionManager;
    public ActionManager ActionManager { get => actionManager; }

    public virtual void Init(BaseActor actor)
    {
        this.actor = actor;
        skillEvent = new EventEmmiter();
        buffManager = new BuffManager();
        buffManager.Init(skillEvent,actor);
        actionManager = new ActionManager();
        actionManager.Init(actor,skillEvent);
        
        effectInfo= new EffectInfo();
        effectInfo.Init(actor.Creature.PivotAget, actionManager);
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
    }

    public virtual void OnSkillAuto()
    {
        actionManager.RegisterSkillAuto();
    }

    public bool? CheckSkillType()
    {
        DesignEnum.SkillType? skillType = actionManager.CheckSkillType(actionManager.NowSkillId);
        if(skillType == null) 
            return null;

        if (skillType == DesignEnum.SkillType.Normal)
        {
            return false;
        }
        else
            return true;

    }

    public virtual float GetSkillAttackRange(int skillId) { return 0; }

    public virtual void OnBuff(int buffId) 
    {
        buffManager.Register(buffId);
    }

    public virtual void RemoveBuff(int buffId) 
    { 

    }

    public virtual void OnEffect(int id)
    {
        effectInfo.SetActiveEffect(id);
    }

    public virtual void OnJudge()
    {
        if (actionManager == null)
            return;

        actionManager.OnJudge();
    }
}
