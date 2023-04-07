using DesignTable;
using Module.Core.Systems.Events;
using Module.Unity.Addressables;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ComUIElemQuestInfo : ComUIBattleElement
{
    [SerializeField] TextMeshProUGUI questName;

    public TextMeshProUGUI QuestName => questName;

    [SerializeField] TextMeshProUGUI questInfo;
    public TextMeshProUGUI QuestInfo => questInfo;

    private List<TextMeshProUGUI> questInfos = new List<TextMeshProUGUI>();

    bool isFirst = true;
    public override void Init()
    {
    }

    public void SetData(string questName, Dictionary<string, IEventArgs> reachQuest)
    {
        this.questName.text = questName;

        foreach (var data in reachQuest.Values)
        {
            TextMeshProUGUI text = null;
            if (isFirst)
            {
                text = questInfo;
                isFirst = false;
            }
            else
            {
                Poolable poolObject = Managers.Pool.Pop(questInfo.gameObject, this.questName.transform);
                poolObject.gameObject.transform.localScale = Vector3.one;
                text = poolObject.GetComponent<TextMeshProUGUI>();
            }
            questInfos.Add(text);
            StringBuilder questString = new StringBuilder();
            EventArgs<int, int> val = (EventArgs<int, int>)data;
            text.text = string.Format("- {0}/{1}", val.Arg1.ToString(), val.Arg2.ToString());
        }
    }


    private void OnDisable()
    {
        for (int i = 0, range = questInfos.Count; i < range; ++i)
        {
            Managers.Pool.Push(questInfos[i].gameObject.GetComponent<Poolable>());
        }

        questInfos.Clear();
    }
}
