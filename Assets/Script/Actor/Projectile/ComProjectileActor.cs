using Module.Unity.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComProjectileActor : ComBaseActor
{
    protected Transform startPos;
    public Transform StartPos => startPos;

    protected float lifeRange = 0;
    public float LifeRange => lifeRange;

    protected float speed;
    public float Speed => speed;

    public override void Init()
    {

    }

    public void SetData(BaseActor actor,Transform startPos, float lifeRange, float speed)
    {
        this.actor = actor;
        this.speed = speed;
        this.startPos = startPos;
        this.lifeRange = lifeRange;

        GameObject obj = ComponentUtil.FindChild(gameObject, "origin");
        obj.transform.rotation = Quaternion.LookRotation(-startPos.forward, Vector3.up);
        transform.position = startPos.position;
    }

    protected override void UpdateComActor()
    {
        if (startPos == null)
            return;
        if (Vector3.Distance(startPos.position, transform.position) <= lifeRange)
        {
            transform.Translate(startPos.forward * speed * Time.deltaTime);
        }
        else
        {
            Managers.Resource.Destory(gameObject);
        }
    }

    protected override void Disable()
    {
        startPos = null;
        actor = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Managers.Object.MyActor.StatusAgent.CalcDecreaseHP(StatusDefine.HPType.NowHP, actor);
            Managers.Resource.Destory(gameObject);
        }
    }

    public override void OnSound()
    {

    }

}
