using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace MonsterState
{
    public class Idle : State<BaseActor>
    {
        MonsterController contoller;
        float nowTime;
        float delayTime;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if(entity.Controller !=null)
            {
                if(contoller == null)
                    contoller = entity.Controller as MonsterController;
            }

            Managers.Ani.Play(entity.Ani, "Idle");
            delayTime = Random.Range(0f, 5.0f);
        }

        public override void Execute(BaseActor entity)
        {
            if(entity == null) 
                return;

            if (CalcDelayTime())
                entity.FSM.ChangeState(Define.ObjectState.Move);
        }

        public override void Exit(BaseActor entity)
        {
            nowTime = 0;
        }

        private bool CalcDelayTime()
        {
            if (contoller.SearchTarget())
                return true;

            if (nowTime <= delayTime)
            {
                nowTime += Time.deltaTime;
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class Move : State<BaseActor>
    {
        MonsterController controller;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if (entity.Controller == null)
                return;
            controller = entity.Controller as MonsterController;

            Managers.Ani.Play(entity.Ani, "Move");
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null) 
                return;

            if (controller.ReachPath)
                entity.FSM.ChangeState(Define.ObjectState.Idle);
            else
            {
                if(controller.CheckAttackRange())
                {

                }
                controller.MovePath();
            }
        }

        public override void Exit(BaseActor entity)
        {
            controller.ReachPath = false;
        }
    }

    public class Attack : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(BaseActor entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(BaseActor entity)
        {
            throw new System.NotImplementedException();
        }
    }


    public class Skill : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(BaseActor entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(BaseActor entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

