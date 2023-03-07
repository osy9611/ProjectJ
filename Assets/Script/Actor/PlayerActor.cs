using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : BaseActor
{
    private DesignTable.user_characterInfo userInfo;
    public DesignTable.user_characterInfo UserInfo { get => userInfo; }

    private int classID;
    public int ClassID { get => classID; set => classID = value; }

    public override void Init()
    {
        base.Init();
        unitType = DesignEnum.UnitType.Character;
        userInfo = Managers.Data.User_characterInfos.Get(classID);

        fsm = new PlayerFSM();
        fsm.Init(this);

        controller = new PlayerController();
        controller.Init(this);

        skillAgent = new SkillAgent();
        skillAgent.Init(this);

        statusAgent = new StatusAgent();
        statusAgent.Init(this);
        statusAgent.SetStatus(userInfo.char_atk, userInfo.char_def, userInfo.char_hp);
    }
    public override void UpdateActor()
    {
        base.UpdateActor();
    }

}
