using Module.Unity.UGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComUIElement : UI_Base
{
    protected BaseActor actor;

    public override void Init()
    {
        actor = Managers.Object.MyActor;
    }
    protected void RegisterUIAction(System.Action action)
    {
        Managers.Object.MyActor.EventEmmiter.AddListener(action.Invoke);
    }
}
