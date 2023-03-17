using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComInteractionActor : ComBaseActor
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
       
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        SetHud(true);

        Managers.Object.MyActor.InteractiveActors.Add(this);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        SetHud(false);
        Managers.Object.MyActor.InteractiveActors.Remove(this);
        Debug.Log(Managers.Object.MyActor.InteractiveActors.Count);
    }

    private void OnDisable()
    {
        SetHud(false);
        if (Managers.Object.MyActor.InteractiveActors.Find(x => x == this) == null)
            return;

        Managers.Object.MyActor.InteractiveActors.Remove(this);
    }

    public virtual void Interactive()
    {
        Debug.Log("상호 작용");
    }

    private void SetHud(bool isShow)
    {
        if (hudUnitInfo == null)
        {
            hudUnitInfo = new ComHudUnitInfo();
            hudUnitInfo.Init(pivotAgent, null);
        }
        else
        {
            hudUnitInfo.ShowInteraction(isShow);
        }
    }
}
