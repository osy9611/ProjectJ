using DesignTable;
using Module.Core.Systems.Collections.Generic;
using Module.Unity.Addressables;
using Module.Unity.Quest;
using Module.Unity.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComUIElemQuest : ComUIBattleElement
{
    public ComUIElemQuestInfo QuestInfoUI;
    public Transform rootQuests;

    [SerializeField] private List<ComUIElemQuestInfo> questInfoUIs = new List<ComUIElemQuestInfo>();

    public GameObject rewardUI;

    public override void Init()
    {
        base.Init();

        questInfoUIs.Add(QuestInfoUI);
        Managers.Pool.CreatePool(QuestInfoUI.gameObject, 2);
        for (int i = 0; i < 2; ++i)
        {
            Poolable pool = Managers.Pool.Pop(QuestInfoUI.gameObject, rootQuests);
            ComUIElemQuestInfo info = pool.GetComponent<ComUIElemQuestInfo>();
            info.transform.localScale= Vector3.one;
            pool.gameObject.SetActive(false);
            questInfoUIs.Add(info);
        }

        AddQuestUI();

        Managers.Quest.EventEmmiter.AddListener(UpdateQuest);
    }

    public void AddQuestUI()
    {
        UnorderedList<IQuest> quests = Managers.Quest.GetQuestList(0);

        int size = 3;
        if (quests.Count <= 2)
        {
            size = quests.Count;
        }

        for (int i = 0; i < size; ++i)
        {
            questInfoUIs[i].SetData(quests[i] as QuestData);
        }
    }

    public void UpdateQuest()
    {
        for (int i = questInfoUIs.Count - 1; i >= 0; --i)
        {
            questInfoUIs[i].UpdateData();
        }
    }

}
