using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Unity.Core;

public class FSM
{
    protected BaseActor actor;
    protected State<BaseActor>[] states;
    protected StateMachine<BaseActor> stateMachine;
    public StateMachine<BaseActor> StateMachine { get => stateMachine; }
    public bool AniEnd { get; set; }
    
    public virtual void Init(BaseActor actor)
    {
        this.actor = actor;
        states = new State<BaseActor>[Enum.GetValues(typeof(Define.ObjectState)).Length];
        stateMachine = new StateMachine<BaseActor>();
    }

    public virtual void Execte()
    {
        stateMachine.Execute();
    }

    public virtual void ChangeState(Define.ObjectState state)
    {
        stateMachine.ChangeState(states[(int)state]);
    }

    public bool CheckCurrentState(Define.ObjectState state)
    {
        if(states.Length < (int)state)
            return false;

        if (stateMachine.GetCurrentState() == states[(int)state])
            return true;

        return false;
    }

    public bool CheckPrevState(Define.ObjectState state)
    {
        if (states.Length < (int)state)
            return false;

        if (stateMachine.GetPrevState() == states[(int)state])
            return true;

        return false;
    }
}
