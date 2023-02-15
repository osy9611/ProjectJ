using DesignTable;
using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager 
{
    int nowSkillId;
    Dictionary<int,BaseAction> actions;
    private EventEmmiter skillEvent;

    public void Init(EventEmmiter skillEvent)
    {
        this.skillEvent = skillEvent;
        actions = new Dictionary<int, BaseAction>();

        List<skillInfo> data = Managers.Data.SkillInfos.GetListById((int)DesignEnum.ClassType.Monk);
        
        foreach (skillInfo info in data) 
        {
            BaseAction baseAction = SetSkill(info);
            if (baseAction == null)
                continue;

            actions.Add(info.skill_Id,baseAction);
        }
    }


    public void RegisterSkill(int skillId)
    {
        if (!actions.ContainsKey(skillId) || nowSkillId == skillId)
            return;

        nowSkillId= skillId;
        skillEvent.AddListener(actions[nowSkillId].Execute);
        RegisterCoolTime(skillId);
    }

    public void RegisterCoolTime(int skillid)
    {
        skillEvent.AddListener(actions[skillid].CalcCoolTime);
    }

    public void UnRegister(int skillId)
    {
        if (nowSkillId != skillId)
            return;
        nowSkillId = -1;
        skillEvent.RemoveListener(actions[skillId].Execute);
    }

    public void UnRegister()
    {
        if (nowSkillId == -1)
            return;
        skillEvent.RemoveListener(actions[nowSkillId].Execute);
    }

    public void UnRegisterCoolTime(int skillId)
    {
        skillEvent.RemoveListener(actions[skillId].CalcCoolTime);
    }
   
    private BaseAction SetSkill(skillInfo skillInfo)
    {
        BaseAction baseAction = null;
        switch((DesignEnum.SkillType)skillInfo.skill_type)
        {
            case DesignEnum.SkillType.Normal:
                baseAction = new Action_Normal();
                break;
            case DesignEnum.SkillType.Melee:
                baseAction = new Action_Melee();
                break;
            case DesignEnum.SkillType.Range:
                baseAction = new Action_Range();
                break;
        }

        baseAction.Init(this,skillInfo);
        return baseAction;
    }

    public DesignEnum.SkillType? CheckSkillType(int skillId)
    {
        if (!actions.ContainsKey(skillId))
            return null;

        return (DesignEnum.SkillType)actions[skillId].SkillInfo.skill_type;
    }

    public DesignEnum.SkillAttackType? GetSKillId()
    {
        if (nowSkillId == -1)
            return null;
        return (DesignEnum.SkillAttackType)actions[nowSkillId].SkillInfo.skill_Id;
    }
}
