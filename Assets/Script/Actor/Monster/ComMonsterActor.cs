using Module.Unity.AI;
using Module.Unity.Pivot;
using Module.Unity.UGUI.Hud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComPathAgent))]
public class ComMonsterActor : ComBaseActor
{
    public override void Init()
    {
        actor = new MonsterActor();
        actor.CharCon = GetComponent<CharacterController>();
        actor.Ani = GetComponent<Animator>();
        actor.Creature = this;
        //actor.Init();

        this.gameObject.layer = LayerMask.NameToLayer("Monster");
        hudUnitInfo = new ComHudUnitInfo();
        hudUnitInfo.Init(pivotAgent,actor);
    }

    protected override void UpdateComActor()
    {
        base.UpdateComActor();
    }

    protected override void LateUpdateComActor()
    {
        base.LateUpdateComActor();
    }
}
