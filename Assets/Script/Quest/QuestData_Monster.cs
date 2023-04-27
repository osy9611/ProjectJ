using Module.Core.Systems.Events;
using Module.Unity.Quest;
using Module.Unity.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData_Monster : QuestData
{
    public override void CheckQuest(string data)
    {
        base.CheckQuest(data);

        if (reachQuest.TryGetValue(data, out var arg))
        {
            Args<int, int, string> val = (Args<int, int, string>)arg;

            if (val.Arg1 < val.Arg2)
                val.Arg1++;

            reachQuest[data] = val;
        }
        else
            return;

        foreach (var quest in reachQuest.Values)
        {
            Args<int, int, string> val = (Args<int, int, string>)quest;

            if (val.Arg1 < val.Arg2)
                return;
        }

        clear = true;
    }

    public override void GetQuestData(short id)
    {
        base.GetQuestData(id);

        JsonUtil.ParseJsonArray(questTarget, "Monster", (array) =>
        {
            if (array != null)
            {
                foreach (var info in array)
                {
                    string monsterID = info["ID"].ToString();
                    int monsterCount = int.Parse(info["Count"].ToString());
                    string monsterName = Managers.Data.Monster_masterInfos.Get(short.Parse(info["ID"].ToString())).mon_name;
                    reachQuest.Add(monsterID, new Args<int, int, string>(0, monsterCount, monsterName));
                }
            }

        });

        RewardData reward = new RewardData();
        JsonUtil.ParseJsonArray(rewardData, "passive", (array) =>
        {
            if (array != null)
            {
                
                foreach (var info in array)
                {
                    if (short.TryParse(info["ID"].ToString(), out short result))
                    {
                        reward.rewardInfos.Add(Managers.Data.PassiveInfos.Get(result));
                    }
                }
                rewardInfos.Add(typeof(DesignTable.passiveInfo), reward);
            }
        });

        if(rewardData.ContainsKey("gatePoint"))
        {
            string gateID = JsonUtil.Parse<string>(rewardData,"gatePoint");
            GameObject gateObject = GameObject.Find(gateID);
            if (gateObject != null) 
            {
                gateObject.SetActive(false);
                reward.GatePoint = gateObject;
            }
        }
    }

    public override void GetReward()
    {
        if (rewardInfos.TryGetValue(typeof(DesignTable.passiveInfo), out var value))
        {
            value.SetData();
        }
        base.GetReward();
    }
}
