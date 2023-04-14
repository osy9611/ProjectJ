using Module.Core.Systems.Events;
using Module.Unity.UGUI;
using Module.Unity.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComUIPopupReward : UI_Popup
{
    ComUIElemReward comUIElemReward = null;
    private enum RewardButtons
    {
        RewardSelectButton1,
        RewardSelectButton2,
        RewardSelectButton3
    }

    private enum RewardText
    {
        RewardInfo1, RewardInfo2, RewardInfo3,
    }

    private enum RewardImange
    {
        RewardImage1, RewardImage2, RewardImage3
    }

    public override void PostInit()
    {
        base.PostInit();
        Bind<Button>(typeof(RewardButtons));
        Bind<Image>(typeof(RewardImange));
        Bind<TextMeshProUGUI>(typeof(RewardText));
    }

    public override void Init()
    {
        base.Init();

    }

    public void SetUIData(int index, string rewardInfo, string rewardImangePath, System.Action<IArgs> callback = null, IArgs args = null)
    {
        Get<Image>(index).sprite = Managers.Atlas.Get(rewardImangePath);
        Get<TextMeshProUGUI>(index).text = rewardInfo;

        Args<int, int>? val = args as Args<int, int>?;

        if (!val.HasValue)
            return;

        Button button = Get<Button>(index);
        button.onClick.AddListener(() => callback(args));
        button.onClick.AddListener(() => Managers.UI.GetElem<ComUIElemReward>().gameObject.SetActive(false));
        button.onClick.AddListener(() => this.gameObject.SetActive(false));

    }

    private void OnDisable()
    {
        ResetUIData();
    }

    private void ResetUIData()
    {
        Button[] buttons = Get<Button>();

        foreach(var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
