using DesignTable;
using Module.Core.Systems.Events;
using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction
{
    protected ActionManager actionManager;
    
    protected skillInfo skillInfo;
    public skillInfo SkillInfo { get => skillInfo; }

    private float nowCoolTime = 0;
    protected float nowjudgeTime = 0;

    public bool IsCool = false;

    public virtual void Init(ActionManager actionManager,skillInfo skillInfo)
    {
        this.actionManager = actionManager;
        this.skillInfo = skillInfo;
    }

    public virtual void Execute()
    {
        if(!skillInfo.skill_judgeAni)
        {
            CalcJudgeTime();
        }
    }

    public virtual void Release()
    {

    }

    protected virtual void CalcJudgeTime()
    {
        if(nowjudgeTime<=skillInfo.skill_judgeTime)
        {
            nowjudgeTime += Time.deltaTime;
        }
        else
        {
            nowjudgeTime = 0;
            OnJudge();
        }
    }

    public virtual void CalcCoolTime()
    {
        if(nowCoolTime <= skillInfo.skill_coolTime)
        {
            nowCoolTime += Time.deltaTime;
            IsCool = true;
        }
        else
        {
            nowCoolTime = 0;
            IsCool = false;
            actionManager.UnRegisterCoolTime(skillInfo.skill_Id);

        }
    }

    public virtual void OnJudge()
    {
        EventArgs<float, float, float> args = new EventArgs<float, float, float>(skillInfo.skill_range, skillInfo.skill_radius, skillInfo.skill_scale);
        List<BaseActor> objs = Managers.Judge.CheckJudge(actionManager.Actor, (DesignEnum.SkillAttackType)skillInfo.skill_attackType, args);
    }
}
