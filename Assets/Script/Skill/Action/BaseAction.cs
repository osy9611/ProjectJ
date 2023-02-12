using DesignTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction
{
    ActionManager actionManager;

    protected skillInfo skillInfo;

    private float nowCoolTime = 0;

    public bool IsCool = false;

    public virtual void Init(ActionManager actionManager,skillInfo skillInfo)
    {
        this.actionManager = actionManager;
        this.skillInfo = skillInfo;
    }

    public abstract void Execute();

    public virtual void Release()
    {
        actionManager.UnRegister(skillInfo.skill_Id);
    }

    public virtual void CalcCoolTime()
    {
        if(nowCoolTime <= skillInfo.skill_coolTime)
        {
            nowCoolTime += Time.deltaTime;
            IsCool = true;
            Debug.Log("스킬 쿨타임 온");
        }
        else
        {
            Debug.Log("스킬 쿨타임 끝");
            nowCoolTime = 0;
            IsCool = false;
            actionManager.UnRegisterCoolTime(skillInfo.skill_Id);

        }
    }


}
