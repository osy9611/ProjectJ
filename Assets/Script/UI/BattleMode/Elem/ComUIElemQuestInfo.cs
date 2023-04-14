using DesignTable;
using Module.Core.Systems.Events;
using Module.Unity.Addressables;
using Module.Unity.Quest;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ComUIElemQuestInfo : ComUIBattleElement
{
    private QuestData questData;

    [SerializeField] TextMeshProUGUI questName;

    public TextMeshProUGUI QuestName => questName;

    [SerializeField] TextMeshProUGUI questInfo;
    public TextMeshProUGUI QuestInfo => questInfo;

    private List<TextMeshProUGUI> questInfos = new List<TextMeshProUGUI>();

    bool isFirst = true;

    public override void Init()
    {
    }

    public void SetData(QuestData questData)
    {
        if (questData == null)
        {
            Debug.LogError("Null QuestData");
            return;
        }
        this.gameObject.SetActive(true);

        this.questData = questData;
        this.questName.text = questData.Name;
        UpdateData(true);
    }


    public void UpdateData()
    {
        if (questData == null)
        {
            Debug.LogError("Null QuestData");
            return;
        }

        if(questData.Clear)
        {
            for (int i = 0, range = questInfos.Count; i < range; ++i)
            {
                Managers.Pool.Push(questInfos[i].gameObject.GetComponent<Poolable>());
            }

            questInfos.Clear();

            this.gameObject.SetActive(false);


            Managers.UI.ActiveElem<ComUIElemReward>((result) =>
            {
                result.ActiveRewardButton(true);
            });

            questData = null;
        }
        else
        {
            UpdateData(false);
        }
    }

    private void UpdateData(bool isCreate = false)
    {
        foreach(var data in questData.ReachQuest.Values)
        {
            TextMeshProUGUI text = null;
            Args<int, int, string> val = (Args<int, int, string>)data;
            if (isCreate)
            {
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
            }
            else
            {
                text = questInfos.Find(x => x.text.Contains(val.Arg3));
            }

            if (text == null)
                continue;

            text.text = string.Format("- {2} {0}/{1}", val.Arg1.ToString(), val.Arg2.ToString(), val.Arg3);
        }
    }

}
