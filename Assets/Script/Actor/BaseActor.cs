using Module.Core.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActor
{
    protected DesignEnum.UnitType unitType;
    public DesignEnum.UnitType UnitType { get => unitType; set => unitType = value; }

    protected FSM fsm;
    public FSM FSM { get => fsm; }

    protected Controller controller;
    public Controller Controller { get => controller; }

    protected SkillAgent skillAgent;
    public SkillAgent SkillAgent { get => skillAgent; }

    protected StatusAgent statusAgent;
    public StatusAgent StatusAgent { get => statusAgent; }

    protected ComBaseActor creature;
    public ComBaseActor Creature { get => creature; set => creature = value; }

    protected CharacterController charCon;
    public CharacterController CharCon { get => charCon; set => charCon = value; }

    protected Animator ani;
    public Animator Ani { get => ani; set => ani = value; }

    protected Vector2 dir;
    public Vector2 Dir { get => dir; set => dir = value; }

    public EventEmmiter EventEmmiter { get => Managers.Object.GetEventEmmiter(this); }
    public virtual void Init()
    {
    }

    public virtual void UpdateActor()
    {
        if (skillAgent == null)
            return;

        if (fsm != null)
            fsm.Execte();
    }
    public virtual void LateUpdateActor() { }

    public virtual void Enable() { }
    public virtual void Disable() { }

}
