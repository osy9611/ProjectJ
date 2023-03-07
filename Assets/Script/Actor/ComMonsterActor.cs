using Module.Unity.AI;
using Module.Unity.Pivot;
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
    }
    protected override void UpdateComActor()
    {
        if (actor == null)
            return;

        actor.UpdateActor();
    }

    protected override void LateUpdateComActor()
    {
        if (actor == null)
            return;

        actor.LateUpdateActor();
    }
}
