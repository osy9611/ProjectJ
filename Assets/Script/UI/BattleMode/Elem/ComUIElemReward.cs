using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComUIElemReward : ComUIBattleElement
{
    private enum Buttons
    {
        RewardButton
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        ActvieAllButton(false);
    }

    public void ActiveRewardButton(bool isActive)
    {
        Get<Button>((int)Buttons.RewardButton).gameObject.SetActive(isActive);
    }

    private void ActvieAllButton(bool isActive)
    {
        for (int i = 0, range = System.Enum.GetNames(typeof(Buttons)).Length; i < range; ++i)
        {
            Get<Button>(i).gameObject.SetActive(isActive);
        }
    }

}
