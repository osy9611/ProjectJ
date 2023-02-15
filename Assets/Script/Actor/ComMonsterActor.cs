using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComMonsterActor : ComBaseActor
{
    public override void Init()
    {
        actor = new MonsterActor();
        actor.Init();
    }
    protected override void UpdateComActor()
    {
        if (actor == null)
            return;

        actor.UpdateActor();
    }

    protected override void LateUpdateComActor()
    {
        if (actor == null)
            return;

        actor.LateUpdateActor();
    }
}
