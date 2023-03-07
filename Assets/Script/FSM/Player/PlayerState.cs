using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerState
{
    public class Idle : State<BaseActor>
    {
        PlayerController controller;
        PlayerFSM fsm;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            Managers.Ani.Play(entity.Ani, "Idle");
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null)
            {
                return;
            }                

            if (controller == null)
            {
                controller = entity.Controller as PlayerController;
            }              

            if (fsm == null)
                fsm = entity.FSM as PlayerFSM;

            if (fsm.CheckSkill()) 
                return;

            Managers.Ani.CheckAndPlay(entity.Ani, "Idle");

            if (controller.IsMove)
            {
                entity.FSM.ChangeState(Define.ObjectState.Move);
            }

            controller.QViewController.Move(Vector2.zero);
            controller.QViewController.Execute();
        }

        public override void Exit(BaseActor entity)
        {
            if (entity == null)
                return;
        }
    } 

    public class Move : State<BaseActor>
    {
        PlayerController controller;
        PlayerFSM fsm;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if (controller == null)
                controller = entity.Controller as PlayerController;
            Managers.Ani.Play(entity.Ani, "Move");
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null)
                return;

            if (fsm == null)
                fsm = entity.FSM as PlayerFSM;

            if(fsm.CheckSkill())
                return;

            if (!controller.IsMove)
            {
                entity.FSM.ChangeState(Define.ObjectState.Idle);
            }

            Managers.Ani.CheckAndPlay(entity.Ani, "Move");

            if (Managers.Input.GetNowContorolScheme() == "PC")
            {
                SetDir(entity,true);
            }

            controller.QViewController.Execute();
        }
        public override void Exit(BaseActor entity)
        {
            controller.QViewController.Move(Vector2.zero);
        }

        private void SetDir(BaseActor actor,bool isMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == null)
                    return;
                Vector2 nowPos = new Vector2(actor.Creature.transform.position.x, actor.Creature.transform.position.z);
                Vector2 hitPoint = new Vector2(hit.point.x, hit.point.z);

                Vector2 dir = hitPoint - nowPos;
                if (isMove)
                    controller.QViewController.Move(dir.normalized);
                else
                    controller.QViewController.Look(dir.normalized);
            }
        }
    }

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

            Managers.Ani.CheckAndPlay(entity.Ani, "Attack");

            if (entity.FSM.AniEnd)
            {
                entity.FSM.ChangeState(Define.ObjectState.Idle);
                return;
            }
        }

        public override void Exit(BaseActor entity)
        {
            entity.SkillAgent.ActionManager.UnRegister();
        }
    }

    public class Skill : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            DesignEnum.SkillID? aniName = entity.SkillAgent.ActionManager.GetSKillId();
            if (aniName == null)
                return;

            Managers.Ani.Play(entity.Ani, aniName.ToString());
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null)
                return;

            DesignEnum.SkillID? aniName = entity.SkillAgent.ActionManager.GetSKillId();
            Managers.Ani.CheckAndPlay(entity.Ani, aniName.ToString());

            if (entity.FSM.AniEnd)
            {
                entity.FSM.ChangeState(Define.ObjectState.Idle);
            }
        }

        public override void Exit(BaseActor entity)
        {
            entity.SkillAgent.ActionManager.UnRegister();
        }
    }
}
