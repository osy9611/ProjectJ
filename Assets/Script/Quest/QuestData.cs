using DesignTable;
using Module.Core.Systems.Collections.Generic;
using Module.Unity.Quest;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : Quest<DesignTable.questInfo>
{
    protected JObject rewardData;
    protected JObject questTarget;

    public override void CheckQuest(string data)
    {

    }

    public override void GetQuestData(short id)
    {
        questInfo = Managers.Data.QuestInfos.Get(id);
        name = questInfo.quest_description;
        this.id = questInfo.quest_Id;
        type = questInfo.quest_type;
        getAutoReward = questInfo.reward_auto;

        if (questInfo.reward != "-")
            rewardData = JObject.Parse(questInfo.reward);
        questTarget = JObject.Parse(questInfo.quest_target);
    }

    public override void GetReward()
    {
        Managers.Quest.RemoveQuest((short)type, this);
    }

}
