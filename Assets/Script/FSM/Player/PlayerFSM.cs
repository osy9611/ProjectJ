using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : FSM
{
    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        states[(int)Define.ObjectState.Idle] = new CommonState.Idle();
        states[(int)Define.ObjectState.Move] = new CommonState.Move();
        states[(int)Define.ObjectState.Attack] = new PlayerState.Attack();
        states[(int)Define.ObjectState.Skill] = new PlayerState.Skill();
        states[(int)Define.ObjectState.Death] = new CommonState.Death();
        states[(int)Define.ObjectState.Spawn] = new CommonState.Spawn();

        stateMachine.Init(actor, states[(int)Define.ObjectState.Idle]);
    }
}
