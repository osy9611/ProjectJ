using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComUIElemReward : ComUIBattleElement
{
    private enum RewardButtons
    {
        RewardButton
    }

    public override void Init()
    {
        Bind<Button>(typeof(RewardButtons));
        ActvieAllButton(false);
    }

    public void ActiveRewardButton(bool isActive)
    {
        Get<Button>((int)RewardButtons.RewardButton).gameObject.SetActive(isActive);
    }

    private void ActvieAllButton(bool isActive)
    {
        Button[] buttons = Get<Button>();
        for (int i = 0, range = buttons.Length; i < range; ++i)
        {
            buttons[i].gameObject.SetActive(isActive);
        }
    }
}
