using Module.Core.Systems.Collections.Generic;
using Module.Unity.Quest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComUIElemQuest : ComUIBattleElement
{
    public ComUIElemQuestInfo QuestInfoUI;

    private List<ComUIElemQuestInfo> questInfoUIs = new List<ComUIElemQuestInfo>();

    public override void Init()
    {
        base.Init();

        if(!questInfoUIs.Contains(QuestInfoUI))
            questInfoUIs.Add(QuestInfoUI);
        Managers.Quest.AddQuest<QuestData_Monster>(0);
        AddQuestUI();
    }

    public void AddQuestUI()
    {
        UnorderedList<IQuest> quests = Managers.Quest.GetQuestList(0);

        QuestData questData = quests[0] as QuestData;

        QuestInfoUI.SetData(questData.Name, questData.ReachQuest);
    }

}
