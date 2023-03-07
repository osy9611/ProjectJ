using Module.Unity.Core;
using Module.Unity.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace MonsterState
{
    public class Idle : State<BaseActor>
    {
        private MonsterFSM fsm;
        private float nowTime;
        private float delayTime;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if(fsm == null)
            {
                if(entity.FSM !=null)
                    fsm = entity.FSM as MonsterFSM;
            }

            Managers.Ani.Play(entity.Ani, "Idle");
            if (entity.FSM.CheckPrevState(Define.ObjectState.Move))
            {
                Util.RandomFloat(0f, 5.0f, out delayTime);
            }
           
        }

        public override void Execute(BaseActor entity)
        {
            if(entity == null) 
                return;

            Managers.Ani.CheckAndPlay(entity.Ani, "Idle");
            fsm.CheckAttackRangeFSM(true, null, (result) =>
            {
                if(!result)
                {
                    if (CalcDelayTime())
                    {
                        entity.FSM.ChangeState(Define.ObjectState.Move);
                    }    
                        
                }
            });
        }

        public override void Exit(BaseActor entity)
        {
            nowTime = 0;
            delayTime = 0;
        }

        private bool CalcDelayTime()
        {
            if (fsm.SearchTarget)
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
        private MonsterFSM fsm;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if (fsm == null)
            {
                if (entity.FSM != null)
                    fsm = entity.FSM as MonsterFSM;
            }
            fsm.CheckAttackRangeFSM(false, Define.ObjectState.Idle);
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null) 
                return;

            fsm.MovePathFSM();
           
        }

        public override void Exit(BaseActor entity)
        {
        }
    }

    public class Attack : State<BaseActor>
    {
        private MonsterFSM fsm;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if (fsm == null)
            {
                if (entity.FSM != null)
                    fsm = entity.FSM as MonsterFSM;
            }

            Managers.Ani.Play(entity.Ani, "Attack");
        }

        public override void Execute(BaseActor entity)
        {
            if (entity == null)
                return;

            fsm.LookAtTargetFSM();
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
        private MonsterFSM fsm;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if (fsm == null)
            {
                if (entity.FSM != null)
                    fsm = entity.FSM as MonsterFSM;
            }

            DesignEnum.SkillID? aniName = entity.SkillAgent.ActionManager.GetSKillId();
            if (aniName == null)
                return;
            Managers.Ani.Play(entity.Ani, aniName.ToString());
        }

        public override void Execute(BaseActor entity)
        {
            fsm.LookAtTargetFSM();
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

    public class Death : State<BaseActor>
    {
        private MonsterFSM fsm;
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            if (fsm == null)
            {
                if (entity.FSM != null)
                    fsm = entity.FSM as MonsterFSM;
            }

            fsm.ResetMovePathFSM();
            
            Managers.Ani.Play(entity.Ani, "Death");
        }

        public override void Execute(BaseActor entity)
        {
            if (entity.FSM.AniEnd)
            {
                entity.FSM.ChangeState(Define.ObjectState.Spawn);
            }
        }

        public override void Exit(BaseActor entity)
        {

        }
    }

}

