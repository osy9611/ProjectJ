using DesignTable;
using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JudgementManager
{
    //args => 1 : search_range, 2 : search_angle
    public BaseActor CheckTarget(BaseActor actor, IArgs args)
    {
        Args<float, float>? val = args as Args<float, float>?;
        if (actor == null || !val.HasValue)
            return null;

        Transform tr = actor.Creature.transform;
        var data = Physics.SphereCastAll(tr.position, val.Value.Arg1, tr.up, 0, 1 << LayerMask.NameToLayer("Player"));
        if (data.Length > 0)
        {
            if (Managers.Object.MyActor.Creature.gameObject == data[0].transform.gameObject)
            {
                return Managers.Object.MyActor;
            }
        }
        return null;
    }


    //args => 1 : skill_range, 2 : skill_radius, 3 : skill_scale
    public List<BaseActor> CheckHit(BaseActor actor, DesignEnum.SkillAttackType attackType, IArgs args)
    {
        Args<float, float, float>? val = args as Args<float, float, float>?;
        if (!val.HasValue)
            return null;

        string checkTarget = "";

        if (Managers.Object.MyActor == actor)
            checkTarget = "Monster";
        else
            checkTarget = "Player";


        switch (attackType)
        {
            case DesignEnum.SkillAttackType.Circle:
                return CalcCircle(checkTarget, actor.Creature.transform, val);

            case DesignEnum.SkillAttackType.Straight:
                return CalcStraight(checkTarget, actor.Creature.transform, val);
        }

        return null;
    }

    public List<BaseActor> CalcCircle(string checkTarget, Transform tr, Args<float, float, float>? val)
    {
        if (string.IsNullOrEmpty(checkTarget))
            return null;


        var datas = Physics.OverlapSphere(tr.position, val.Value.Arg1, 1 << LayerMask.NameToLayer(checkTarget));
        List<BaseActor> result = CheckAngle(checkTarget, tr, datas, val.Value.Arg2);
        return result;
    }

    public List<BaseActor> CalcStraight(string checkTarget, Transform tr, Args<float, float, float>? val)
    {
        if (string.IsNullOrEmpty(checkTarget))
            return null;

        var datas = Physics.OverlapBox(tr.position, new Vector3(val.Value.Arg3, val.Value.Arg3, val.Value.Arg3), Quaternion.identity, LayerMask.GetMask(checkTarget));
        List<BaseActor> result = CheckAngle(checkTarget, tr, datas, val.Value.Arg2);
        return result;
    }

    private BaseActor CheckActor(Collider info, Transform tr, string checkTarget)
    {
        BaseActor actor = null;
        if (checkTarget == "Player")
        {
            actor = Managers.Object.FindById(info.transform.gameObject, true);
        }
        else
        {
            actor = Managers.Object.FindById(info.transform.gameObject, false);
        }

        if (!actor.FSM.CheckCurrentState(Define.ObjectState.Death))
            Managers.Effect.Get(4, info.ClosestPoint(tr.position));

        return actor;
    }

    private List<BaseActor> CheckAngle(string checkTarget, Transform myTransform, Collider[] datas, float radius)
    {
        List<BaseActor> result = new List<BaseActor>();
        foreach (var info in datas)
        {
            Vector3 dir = (info.transform.position - myTransform.position).normalized;
            float dot = Vector3.Dot(myTransform.forward, dir);
            if (dot > Mathf.Cos(radius / 2) * Mathf.Deg2Rad)
            {
                BaseActor actor = CheckActor(info, myTransform, checkTarget);

                if (actor != null)
                {
                    result.Add(actor);
                }
            }
        }

        return result;
    }
}
