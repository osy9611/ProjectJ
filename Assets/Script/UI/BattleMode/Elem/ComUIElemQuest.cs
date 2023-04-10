using Module.Core.Systems.Collections.Generic;
using Module.Unity.Quest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComUIElemQuest : ComUIBattleElement
{
    public ComUIElemQuestInfo QuestInfoUI;

    [SerializeField] private List<ComUIElemQuestInfo> questInfoUIs = new List<ComUIElemQuestInfo>();

    public GameObject rewardUI;

    public override void Init()
    {
        base.Init();

        if (!questInfoUIs.Contains(QuestInfoUI))
            questInfoUIs.Add(QuestInfoUI);
       
        AddQuestUI();

        Managers.Quest.EventEmmiter.AddListener(UpdateQuest);
    }

    public void AddQuestUI()
    {
        UnorderedList<IQuest> quests = Managers.Quest.GetQuestList(0);
        QuestInfoUI.SetData(quests[0] as QuestData);
    }

    public void UpdateQuest()
    {
        for (int i = questInfoUIs.Count - 1; i >= 0; --i)
        {
            questInfoUIs[i].UpdateData();
        }
    }

}
