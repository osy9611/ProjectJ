using DesignTable;
using Module.Core.Systems.Events;
using Module.Unity.Core;
using Module.Unity.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction
{
    protected ActionManager actionManager;

    protected skillInfo skillInfo;
    public skillInfo SkillInfo { get => skillInfo; }

    private float nowCoolTime = 0;
    public float NowCoolTime { get => (nowCoolTime / skillInfo.skill_coolTime); }
    protected float nowjudgeTime = 0;

    public bool IsCool = false;

    protected List<BaseActor> checkActors = new List<BaseActor>();

    public virtual void Init(ActionManager actionManager, skillInfo skillInfo)
    {
        this.actionManager = actionManager;
        this.skillInfo = skillInfo;
    }

    public virtual void Execute()
    {
        if (!skillInfo.skill_judgeAni)
        {
            CalcJudgeTime();
        }
    }

    public virtual void Release()
    {

    }

    protected virtual void CalcJudgeTime()
    {
        if (nowjudgeTime <= skillInfo.skill_judgeTime)
        {
            nowjudgeTime += Time.deltaTime;
        }
        else
        {
            nowjudgeTime = 0;
            OnJudge();
            if (!skillInfo.sound_Ani)
                OnSound();
        }
    }

    public virtual void CalcCoolTime()
    {
        if (nowCoolTime <= skillInfo.skill_coolTime)
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
        checkActors.Clear();
        EventArgs<float, float, float> args = new EventArgs<float, float, float>(skillInfo.skill_range, skillInfo.skill_radius, skillInfo.skill_scale);
        checkActors = Managers.Judge.CheckHit(actionManager.Actor, (DesignEnum.SkillAttackType)skillInfo.skill_attackType, args);

        if (checkActors == null)
            return;

        BaseActor actor = actionManager.Actor;
        foreach (var checkActor in checkActors)
        {
            if (checkActor.FSM.CheckPrevState(Define.ObjectState.Death))
                continue;

            OnHitSound();
            checkActor.StatusAgent.CalcDecreaseHP(StatusDefine.HPType.NowHP, actor);
        }
    }

    public virtual void OnSound()
    {
        Managers.Sound.Play(skillInfo.sound_Res, Sound.FX, 1);
    }

    public virtual void OnHitSound()
    {
        Managers.Sound.Play(skillInfo.hitSound_Res, Sound.FX, 1);
    }
}
