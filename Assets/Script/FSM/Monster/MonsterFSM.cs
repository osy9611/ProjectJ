using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : FSM
{
    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        states[(int)Define.ObjectState.Idle] = new MonsterState.Idle();
        states[(int)Define.ObjectState.Move] = new MonsterState.Move();
        states[(int)Define.ObjectState.Attack] = new MonsterState.Attack();
        states[(int)Define.ObjectState.Skill] = new MonsterState.Skill();
        states[(int)Define.ObjectState.Death] = new CommonState.Death();
        states[(int)Define.ObjectState.Spawn] = new CommonState.Spawn();

        stateMachine.Init(actor, states[(int)Define.ObjectState.Idle]);
    }

    public void RandomDelay()
    {

    }
}
