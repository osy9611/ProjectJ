using Module.Core.Systems.Events;
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
            EventArgs<int, int> val = (EventArgs<int, int>)arg;

            if (val.Arg1 < val.Arg2)
                val.Arg1++;

            reachQuest[data] = val;
        }

        foreach(var quest in reachQuest.Values)
        {
            EventArgs<int, int> val = (EventArgs<int, int>)quest;

            if (val.Arg1 < val.Arg2)
                return;
        }

        clear = true;
    }

    public override void GetQuestData(short id)
    {
        base.GetQuestData(id);

        if (questTarget.TryGetValue("Monster", out var value))
        {
            JArray array = JArray.Parse(value.ToString());

            foreach (var info in array)
            {
                string monsterID = info["ID"].ToString();
                int monsterCount = int.Parse(info["Count"].ToString());
                reachQuest.Add(monsterID, new EventArgs<int, int>(0, monsterCount));
            }
        }
    }

    public override void GetReward()
    {
        Debug.Log("클리어 보상");
     
        base.GetReward();

    }
}
