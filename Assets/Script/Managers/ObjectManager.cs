using DesignTable;
using Module.Core.Systems;
using Module.Core.Systems.Events;
using Module.Unity.AI;
using Module.Unity.Custermization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    private PlayerActor myActor;
    public PlayerActor MyActor { get => myActor; set => myActor = value; }

    Dictionary<GameObject, BaseActor> objects = new Dictionary<GameObject, BaseActor>();

    Dictionary<BaseActor, EventEmmiter> eventEmmiters = new Dictionary<BaseActor, EventEmmiter>();


    public void Execute()
    {
        foreach (var events in eventEmmiters.Values)
        {
            if (events != null)
                events.Invoke();
        }
    }

    public void Add(BaseActor actor, bool myPlayer = false)
    {
        if (actor == null)
            Debug.LogError("Null Actor");

        EventEmmiter eventEmmiter = new EventEmmiter();
        eventEmmiters.Add(actor, eventEmmiter);

        if (myPlayer)
            this.myActor = actor as PlayerActor;

        objects.Add(actor.Creature.gameObject, actor);
    }

    public void Remove(GameObject id)
    {
        if (!objects.ContainsKey(id))
            return;

        objects.Remove(id);
    }

    public BaseActor FindById(GameObject id, bool checkMyPlayer = false)
    {
        if (checkMyPlayer)
        {
            if (myActor.Creature.gameObject == id)
                return myActor;
        }
        else
        {
            if (!objects.ContainsKey(id))
            {
                return null;
            }

            return objects[id];

        }
        return null;
    }

    public void Clear()
    {
        objects.Clear();
    }


    public void LoadMonster(DesignEnum.FieldType type, bool donDestory = false)
    {
        List<monster_deployInfo> deployInfos = Managers.Data.Monster_deployInfos.GetListById((short)type);
        GameObject root = null;

        if (!donDestory)
        {
            root = new GameObject();
            root.name = "Pools";
        }

        foreach (var info in deployInfos)
        {
            monster_masterInfo monMasterInfo = Managers.Data.Monster_masterInfos.Get(info.mon_id);
            List<PathInfo> pathInfo = Managers.Data.DataAssets.PathData.Get(info.mon_id);


            switch ((DesignEnum.MonsterType)info.mon_type)
            {
                case DesignEnum.MonsterType.FieldNormal:
                case DesignEnum.MonsterType.DungeonNormal:
                    monster_normalInfo monNormalInfo = Managers.Data.Monster_normalInfos.Get(info.mon_id);

                    foreach (var path in pathInfo)
                    {
                        GameObject obj = null;
                        if (root == null)
                            obj = Managers.Resource.LoadAndPool(monMasterInfo.mon_prefab, null);
                        else
                            obj = Managers.Resource.LoadAndPool(monMasterInfo.mon_prefab, root.transform);

                        ComMonsterActor comActor = obj.GetComponent<ComMonsterActor>();
                        if (comActor == null)
                            continue;

                        comActor.Init();
                        MonsterActor actor = comActor.Actor as MonsterActor;
                        if (actor == null)
                            continue;

                        actor.ModelID = info.mon_id;
                        actor.SpawnTime = monNormalInfo.mon_spawnTime;
                        actor.Init();
                        (actor.Controller as MonsterController).SetPath(path);
                        Add(actor);
                    }
                    break;

                case DesignEnum.MonsterType.FieldBoss:
                case DesignEnum.MonsterType.DungeonBoss:
                    break;
            }
        }
    }

    public void LoadPlayer(int charId, bool myPlayer = false)
    {
        user_characterInfo info = Managers.Data.User_characterInfos.Get(charId);

        if (info == null)
            return;

        GameObject obj = Managers.Resource.LoadAndPool(info.char_prefab, null, 1);
        ComPlayerActor comActor = obj.GetComponent<ComPlayerActor>();
        if (comActor == null)
            return;
        comActor.Init();
        PlayerActor actor = comActor.Actor as PlayerActor;
        actor.ClassID = info.char_classId;
        //actor.Init();

        Add(actor, myPlayer);
    }

    public void InitPlayer()
    {
        myActor.Creature.transform.position = Vector3.zero;
        myActor.Creature.transform.rotation = Quaternion.identity;
        myActor.Creature.gameObject.SetActive(true);
        myActor.Init();
    }

    public EventEmmiter GetEventEmmiter(BaseActor actor)
    {
        if(eventEmmiters.TryGetValue(actor, out EventEmmiter result))
        {
            return result;
        }

        return null;
    }

}
