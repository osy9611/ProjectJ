using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerFSM : FSM
{
    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        states[(int)Define.ObjectState.Idle] = new PlayerState.Idle();
        states[(int)Define.ObjectState.Move] = new PlayerState.Move();
        states[(int)Define.ObjectState.Attack] = new PlayerState.Attack();
        states[(int)Define.ObjectState.Skill] = new PlayerState.Skill();
        states[(int)Define.ObjectState.Death] = new CommonState.Death();
        states[(int)Define.ObjectState.Spawn] = new CommonState.Spawn();

        stateMachine.Init(actor, states[(int)Define.ObjectState.Idle]);
    }

    public bool CheckSkill()
    {
        bool? checkSkill = actor.SkillAgent.CheckSkillType();
        if (checkSkill != null)
        {
            if (checkSkill == false)
            {
                actor.FSM.ChangeState(Define.ObjectState.Attack);
            }
            else
            {
               actor.FSM.ChangeState(Define.ObjectState.Skill);
            }

            return true;
        }

        return false;
    }

    public void CheckAndPlay(string name)
    {
        if (Managers.Ani.CheckPlayAniName(actor.Ani, name))
            return;
        Managers.Ani.Play(actor.Ani, name);
    }

}
