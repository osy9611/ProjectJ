using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComProjectileActor : ComBaseActor
{
    public BaseActor rootActor;

    public override void Init()
    {
    }

    public void InitRootActor(BaseActor actor)
    {
        this.rootActor = actor;
    }

    public override void OnEffect(int id)
    {

    }

    public override void OnJudge()
    {

    }

    public override void OnSound()
    {

    }

}
