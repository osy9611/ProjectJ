using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComPlayerActor : ComBaseActor
{    
    protected Vector3 dir;
    public Vector3 Dir { get => dir; set => dir = value; }

    public override void Init()
    {
        actor = new PlayerActor();
        actor.CharCon = GetComponent<CharacterController>();
        actor.Ani = GetComponent<Animator>();
        actor.Creature = this;
        Managers.Object.MyActor = actor as PlayerActor;

        this.gameObject.layer = LayerMask.NameToLayer("Player");

        hudUnitInfo = new ComHudUnitInfo();
        hudUnitInfo.Init(pivotAgent, actor);
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
