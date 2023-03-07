using Module.Unity.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterActor : BaseActor
{
    DesignTable.monster_masterInfo monsterInfo;
    public DesignTable.monster_masterInfo MonsterInfo { get => monsterInfo; }

    private short modelID;
    public short ModelID { get => modelID; set => modelID = value; }

    private float spawnTime;
    public float SpawnTime { get => spawnTime; set=> spawnTime = value; }

    public override void Init()
    {
        base.Init();
        UnitType = DesignEnum.UnitType.Monster;
        monsterInfo = Managers.Data.Monster_masterInfos.Get(modelID);

        skillAgent = new SkillAgent();
        skillAgent.Init(this);

        controller = new MonsterController();
        controller.Init(this);

        fsm = new MonsterFSM();
        fsm.Init(this);

        statusAgent = new StatusAgent();
        statusAgent.Init(this);
        statusAgent.SetStatus(monsterInfo.mon_atk, monsterInfo.mon_def, monsterInfo.mon_hp);

    }
    public override void UpdateActor()
    {
        base.UpdateActor();
    }

}
