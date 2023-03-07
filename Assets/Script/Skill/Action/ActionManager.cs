using DesignTable;
using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
    private int nowSkillId = -1;
    public int NowSkillId { get => nowSkillId; }

    private Dictionary<int, BaseAction> actions;
    private BaseActor actor;
    public BaseActor Actor { get => actor; }

    public void Init(BaseActor actor)
    {
        this.actor = actor;
        actions = new Dictionary<int, BaseAction>();
        List<skillInfo> data = null;
        if (actor.UnitType== DesignEnum.UnitType.Character)
        {
            PlayerActor playerActor = actor as PlayerActor;
            if (playerActor == null)
                return;
            data = Managers.Data.SkillInfos.GetListById(playerActor.ClassID,(int)DesignEnum.UnitType.Character);
        }
        else
        {
            MonsterActor monsterActor = actor as MonsterActor;
            if(monsterActor == null) 
                return;
            data = Managers.Data.SkillInfos.GetListById(monsterActor.ModelID,(int)DesignEnum.UnitType.Monster);
        }

        if (data == null)
            return;
        
        foreach (skillInfo info in data)
        {
            BaseAction baseAction = SetSkill(info);
            if (baseAction == null)
                continue;

            actions.Add(info.skill_Id, baseAction);
        }
    }


    public void RegisterSkill(int skillId)
    {
        if (!actions.ContainsKey(skillId) || nowSkillId == skillId)
            return;

        if (actions[skillId].IsCool)
            return;

        nowSkillId = skillId;
        actor.EventEmmiter.AddListener(actions[nowSkillId].Execute);
        RegisterCoolTime(nowSkillId);
    }

    public void RegisterSkillAuto()
    {
        if (actions == null)
            return;

        if (actions.Count == 0)
            return;

        foreach(var action in actions.Values)
        {
            if (action.IsCool)
                continue;

            int skillId = action.SkillInfo.skill_Id;
            RegisterSkill(skillId);
            break;
        }
    }


    public void RegisterCoolTime(int skillid)
    {
        actor.EventEmmiter.AddListener(actions[skillid].CalcCoolTime);
    }

    public void UnRegister(int skillId)
    {
        if (nowSkillId != skillId)
            return;
        nowSkillId = -1;
        actor.EventEmmiter.RemoveListener(actions[skillId].Execute);
    }

    public void UnRegister()
    {
        if (nowSkillId == -1)
            return;
        actor.EventEmmiter.RemoveListener(actions[nowSkillId].Execute);
        actions[nowSkillId].Release();
        nowSkillId = -1;
    }

    public void UnRegisterCoolTime(int skillId)
    {
        actor.EventEmmiter.RemoveListener(actions[skillId].CalcCoolTime);
    }

    private BaseAction SetSkill(skillInfo skillInfo)
    {
        BaseAction baseAction = null;
        switch ((DesignEnum.SkillType)skillInfo.skill_type)
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

        baseAction.Init(this, skillInfo);
        return baseAction;
    }

    public DesignEnum.SkillType? CheckSkillType(int skillId)
    {
        if (!actions.ContainsKey(skillId))
            return null;

        return (DesignEnum.SkillType)actions[skillId].SkillInfo.skill_type;
    }

    public DesignEnum.SkillID? GetSKillId()
    {
        if (nowSkillId == -1)
            return null;
        return (DesignEnum.SkillID)actions[nowSkillId].SkillInfo.skill_Id;
    }

    public BaseAction GetAction(int id)
    {
        if (!actions.ContainsKey(id))
            return null;

        return actions[id];
    }

    public float GetCoolTime(int id)
    {
        if (!actions.ContainsKey(id))
            return -1f;

        return actions[id].NowCoolTime;
    }

    public void OnJudge()
    {
        if (nowSkillId == -1)
            return;

        actions[nowSkillId].OnJudge();
    }
}
