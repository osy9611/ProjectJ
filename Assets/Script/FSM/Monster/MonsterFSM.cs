using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MonsterFSM : FSM
{
    private MonsterController controller;
    public bool SearchTarget { get => controller.SearchTarget(); }
    public bool ReachTarget { get => controller.ReachTarget; }

    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        states[(int)Define.ObjectState.Idle] = new MonsterState.Idle();
        states[(int)Define.ObjectState.Move] = new MonsterState.Move();
        states[(int)Define.ObjectState.Attack] = new MonsterState.Attack();
        states[(int)Define.ObjectState.Skill] = new MonsterState.Skill();
        states[(int)Define.ObjectState.Death] = new MonsterState.Death();
        states[(int)Define.ObjectState.Spawn] = new CommonState.Spawn();

        controller = actor.Controller as MonsterController;

        stateMachine.Init(actor, states[(int)Define.ObjectState.Idle]);
    }

    public void MovePathFSM()
    {
        controller.MovePath();
        if (controller.ReachTarget)
        {
            ChangeState(Define.ObjectState.Idle);
            return;
        }
        Managers.Ani.CheckAndPlay(actor.Ani, "Move");
    }

    public void ResetMovePathFSM()
    {
        controller.ResetNavigation();
    }

    public void LookAtTargetFSM()
    {
        controller.LookAtTarget();
    }

    public void CheckAttackRangeFSM(bool checkSkill, Define.ObjectState? checkType, System.Action<bool> callback = null)
    {
        bool result = controller.CheckAttackRange();
        if (callback != null)
            callback(result);
        if (result)
        {
            if (checkSkill)
            {
                CheckSkill();
                return;
            }
            if (checkType == null)
                return;

            ChangeState((Define.ObjectState)checkType);
        }
    }

    public override bool CheckSkill()
    {
        actor.SkillAgent.OnSkillAuto();
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

   
}
