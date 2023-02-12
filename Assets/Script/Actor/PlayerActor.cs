using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor
{
    public override void Init()
    {
        skillAgent = new SkillAgent();
        skillAgent.Init();
    }
    public override void UpdateActor()
    {
        if (skillAgent == null)
            return;
        skillAgent.Execute();
    }

    public override void LateUpdateActor()
    {

    }

}
