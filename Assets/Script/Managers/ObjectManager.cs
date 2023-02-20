using Module.Core.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private PlayerActor myActor;
    public PlayerActor MyActor { get => myActor; set => myActor = value; }

    Dictionary<string,BaseActor> objects = new Dictionary<string, BaseActor>();

    public void Add(BaseActor actor, bool myPlayer =false)
    {
        if (myPlayer)
            this.myActor = actor as PlayerActor;

        Guid128 id = Guid128.Generate();
        actor.Creature.gameObject.name = id.ToString();
        objects.Add(id.ToString(), actor);
    }

    public void Remove(string id)
    {
        if (!objects.ContainsKey(id))
            return;

        objects.Remove(id);
    }

    public BaseActor FindById(string id)
    {
        if(!objects.ContainsKey(id))
        {
            return null;
        }

        return objects[id];
    }

    public void Clear()
    {
        objects.Clear();
    }


}
