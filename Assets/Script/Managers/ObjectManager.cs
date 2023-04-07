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


    public void LoadMonster(DesignEnum.FieldType type)
    {
        List<monster_deployInfo> deployInfos = Managers.Data.Monster_deployInfos.GetListById((short)type);

        GameObject root = null;
        root = new GameObject();
        root.name = "Monsters";
        GameObject.DontDestroyOnLoad(root);

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
                        CreateMonster(root.transform, path, monMasterInfo.mon_prefab, info.mon_id, monNormalInfo.mon_spawnTime);
                    }
                    break;

                case DesignEnum.MonsterType.FieldBoss:
                case DesignEnum.MonsterType.DungeonBoss:
                    monster_bossInfo monBossInfo = Managers.Data.Monster_bossInfos.Get(info.mon_id);

                    foreach (var path in pathInfo)
                    {
                        CreateMonster(root.transform, path, monMasterInfo.mon_prefab, info.mon_id, monBossInfo.mon_spawnTime,
                            monBossInfo.mon_spawnDayNight != -1 ? (DesignEnum.TimeType)monBossInfo.mon_spawnDayNight : null);
                    }

                    break;
            }
        }
    }

    private void CreateMonster(Transform root, PathInfo path, string prefabPath, short monId, float spawnTime, DesignEnum.TimeType? timeType = null)
    {
        GameObject obj = null;
        obj = Managers.Resource.LoadAndPool(prefabPath, root.transform);

        ComMonsterActor comActor = obj.GetComponent<ComMonsterActor>();
        if (comActor == null)
            return;

        comActor.Init();
        MonsterActor actor = comActor.Actor as MonsterActor;
        if (actor == null)
            return;

        actor.ModelID = monId;
        actor.SpawnTime = spawnTime;
        actor.TimeType = timeType;
        actor.Init();
        (actor.Controller as MonsterController).SetPath(path);
        Add(actor);
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
        if (eventEmmiters.TryGetValue(actor, out EventEmmiter result))
        {
            return result;
        }

        return null;
    }

}
