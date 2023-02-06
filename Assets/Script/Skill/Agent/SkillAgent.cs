using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAgent
{
    EventEmmiter skillEvent;
    BuffManager buffManager;

    public virtual void Init()
    {
        buffManager = new BuffManager();
        buffManager.Init(this);
        skillEvent = new EventEmmiter();
        skillEvent.AddListener(buffManager.Excute);
    }

    public virtual void Execute() 
    {
        skillEvent.Invoke();
    }

    public virtual void ResetData() { }

    public virtual void CheckAttackRange() { }

    public virtual void OnAttack(int skillId) { }

    public virtual float GetSkillAttackRange(int skillId) { return 0; }

    public virtual void AddBuff(int buffId) 
    {
        buffManager.Add(buffId);
    }

    public virtual void RemoveBuff(int buffId) 
    { 

    }

}
