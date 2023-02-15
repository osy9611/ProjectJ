using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private PlayerActor myActor;
    public PlayerActor MyActor { get => myActor; set => myActor = value; }

    Dictionary<int,BaseActor> objects = new Dictionary<int, BaseActor>();


}
