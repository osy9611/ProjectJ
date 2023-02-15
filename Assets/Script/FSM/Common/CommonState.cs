using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonState
{
    public class Idle : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            if (entity == null)
                return;

            Managers.Ani.Play(entity.Ani, "Idle");
        }

        public override void Execute(BaseActor entity)
        {

        }

        public override void Exit(BaseActor entity)
        {

        }
    }

    public class Move : State<BaseActor>
    {
        public override void Enter(BaseActor entity)
        {
            if(entity == null) 
                return;

            Managers.Ani.Play(entity.Ani, "Move");
        }

        public override void Execute(BaseActor entity)
        { 

        }

        public override void Exit(BaseActor entity)
        {

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

    public class Death : State<BaseActor>
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

    public class Spawn : State<BaseActor>
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
