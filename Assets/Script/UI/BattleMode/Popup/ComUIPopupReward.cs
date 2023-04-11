using Module.Unity.UGUI;
using TMPro;
using UnityEngine.UI;

public class ComUIPopupReward : UI_Popup
{
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
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(RewardButtons));
        Bind<Image>(typeof(RewardImange));
        Bind<TextMeshProUGUI>(typeof(RewardText));
    }
}
