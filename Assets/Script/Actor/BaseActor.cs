using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActor
{
    protected SkillAgent skillAgent;
    public SkillAgent SkillAgent { get => skillAgent; }

    protected ComBaseActor creature;
    public ComBaseActor Creature { get => creature; }

    protected CharacterController charCon;
    public CharacterController CharCon { get => charCon; set => charCon = value; }

    protected Vector3 dir;
    public Vector3 Dir { get => dir; set => dir = value; }

    public abstract void Init();
    public abstract void UpdateActor();
    public abstract void LateUpdateActor();
}
