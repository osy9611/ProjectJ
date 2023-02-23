using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Melee : BaseAction
{
    bool isFirst = false;
    Vector3 prevPos;

    public override void Execute()
    {
        base.Execute();
        if (skillInfo.skill_dash)
            DashExecute();
    }

    private void DashExecute()
    {
        if(!isFirst)
        {
            prevPos = actionManager.Actor.Creature.transform.position;
            isFirst = true;
        }

        if (checkActors.Count != 0)
            return;

        Transform nowPos = actionManager.Actor.Creature.transform;

        if (Vector3.Distance(prevPos, nowPos.position) < skillInfo.skill_range)
        {
            Vector3 dir = new Vector3(nowPos.forward.x, -1.0f, nowPos.forward.z);
            actionManager.Actor.CharCon.Move(dir * Time.deltaTime * skillInfo.skill_dashSpeed);
        }
    }

    public override void Release()
    {
        isFirst = false;
    }

}
