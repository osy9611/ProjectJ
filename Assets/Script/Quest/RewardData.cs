using Module.Core.Systems.Events;
using Module.Unity.Quest;
using Module.Unity.Utils;
using System.Collections.Generic;
using UnityEngine;

public class RewardData : Reward<DesignTable.passiveInfo>
{
    private GameObject gatePoint;
    public GameObject GatePoint { get=>gatePoint; set => gatePoint = value; }

    public override void SetData()
    {
        base.SetData();
        List<int> indexs = new List<int>() { 0, 1, 2 };
        if (rewardInfos.Count > 3)
            indexs = Util.RandomDupilcate(0, rewardInfos.Count, 3);
        for (int i = 0, range = indexs.Count; i < range; ++i)
        {
            int rate = Random.Range(0, rewardInfos[i].status_Arg);
            string info = string.Format("{0} + {1}", rewardInfos[i].passive_name, rate);
            string imagePath = rewardInfos[i].image_Res;
            Args<int, int> args = new Args<int, int>(i, rate);
            Managers.UI.ActivePopup<ComUIPopupReward>((result) =>
                {
                    result.SetUIData(i, info, imagePath, GetReward, args);
                });
        }
        gatePoint.SetActive(true);
    }

    //Arg1 : index, Arg2 : rate
    protected override void GetReward(IArgs args)
    {
        Args<int, int>? val = args as Args<int, int>?;

        if (!val.HasValue)
            return;

        if (Managers.Object.MyActor == null)
            return;

        if (rewardInfos[val.Value.Arg1].status_Type == (int)DesignEnum.PassiveType.HP)
            Managers.Object.MyActor.StatusAgent.IncreaseHP(StatusDefine.HPType.MaxHP, val.Value.Arg2);
        else
        {
            DesignEnum.AttributeId? id = null;

            switch ((DesignEnum.PassiveType)rewardInfos[val.Value.Arg1].status_Type)
            {
                case DesignEnum.PassiveType.ATK:
                    id = DesignEnum.AttributeId.Atk;
                    break;
                case DesignEnum.PassiveType.DEF:
                    id = DesignEnum.AttributeId.Def;
                    break;
            }

            if (id != null)
                Managers.Object.MyActor.StatusAgent.IncreaseStatus(StatusDefine.StatusGroupId.Passive, (DesignEnum.AttributeId)id, val.Value.Arg2);
        }
    }
}
