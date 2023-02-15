using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{


    public class Attack : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;
            Managers.Ani.Play(entity.Ani, "Attack");
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null)
                return;

            if(entity.FSM.AniEnd)
            {
                entity.FSM.ChangeState(Define.ObjectState.Idle);
                entity.SkillAgent.ActionManager.UnRegister();
                return;
            }
        }

        public override void Exit(BaseActor entity)
        {

        }
    }

    public class Skill : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;
            DesignEnum.SkillAttackType? aniName = entity.SkillAgent.ActionManager.GetSKillId();

            if (aniName == null)
                return;

            Debug.Log(aniName.ToString());
            Managers.Ani.Play(entity.Ani, aniName.ToString());
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null)
                return;

            if (entity.FSM.AniEnd)
            {
                entity.SkillAgent.ActionManager.UnRegister();
                entity.FSM.ChangeState(Define.ObjectState.Idle);
            }
        }

        public override void Exit(BaseActor entity)
        {

        }
    }
}
