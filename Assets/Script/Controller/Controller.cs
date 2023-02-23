using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller 
{
    protected BaseActor actor;

    protected float gravity = -1.0f;
    public float Gravity { get => gravity; }
    protected float speed;
    public float Speed { get => speed; }

    public virtual void Init(BaseActor actor)
    {
        this.actor = actor;
    }

    public virtual void Resset() { }

}
