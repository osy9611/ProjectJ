using DesignTable;
using Module.Unity.Pivot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Range : BaseAction
{
    private projectileInfo projectile = null;
    PivotInfo shotPoint;

    public override void Init(ActionManager actionManager, skillInfo skillInfo)
    {
        base.Init(actionManager, skillInfo);
        shotPoint = actionManager.Actor.Creature.PivotAget.GetPivotInfo(skillInfo.skill_Id);
        projectile = skillInfo.projectile_Id_ref;
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void OnJudge()
    {
        ComProjectileActor comProjectile =Managers.Resource.LoadAndPop<ComProjectileActor>(projectile.projectile_path);
       
        if (comProjectile == null)
            return;

        comProjectile.SetData(actionManager.Actor, shotPoint.PivotTr, skillInfo.skill_range, projectile.projectile_speed);
    }
}
