using DesignTable;
using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JudgementManager
{
    //args => 1 : search_range, 2 : search_angle
    public BaseActor CheckTarget(BaseActor actor, IEventArgs args)
    {
        EventArgs<float, float>? val = args as EventArgs<float, float>?;
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
    public List<BaseActor> CheckHit(BaseActor actor, DesignEnum.SkillAttackType attackType, IEventArgs args)
    {
        EventArgs<float, float, float>? val = args as EventArgs<float, float, float>?;
        if (!val.HasValue)
            return null;

        switch (attackType)
        {
            case DesignEnum.SkillAttackType.Circle:
                return CalcCircle(actor.Creature.transform, val);

            case DesignEnum.SkillAttackType.Straight:
                return CalcStraight(actor.Creature.transform, val);
        }

        return null;
    }

    public List<BaseActor> CalcCircle(Transform tr, EventArgs<float, float, float>? val)
    {
        List<BaseActor> result = new List<BaseActor>();
        var datas = Physics.SphereCastAll(tr.position, val.Value.Arg1, tr.forward, 0, 1 << LayerMask.NameToLayer("Monster"));
        foreach (var info in datas)
        {
            Vector3 dir = (info.transform.position - tr.position).normalized;
            float dot = Vector3.Dot(tr.forward, dir);
            if (dot > Mathf.Cos(val.Value.Arg2 / 2) * Mathf.Deg2Rad)
            {
                BaseActor actor = Managers.Object.FindById(info.transform.gameObject.name);
                if (actor != null)
                {
                    result.Add(actor);
                }
            }
        }

        return result;
    }

    public List<BaseActor> CalcStraight(Transform tr, EventArgs<float, float, float>? val)
    {
        List<BaseActor> result = new List<BaseActor>();
        var datas = Physics.BoxCastAll(tr.position, tr.lossyScale, tr.forward, tr.rotation, val.Value.Arg3, 1 << LayerMask.NameToLayer("Monster"));
        foreach (var info in datas)
        {
            Debug.Log(info);
            BaseActor actor = Managers.Object.FindById(info.transform.gameObject.name);
            if (actor != null)
            {
                result.Add(actor);
            }
        }
        return result;
    }
}
