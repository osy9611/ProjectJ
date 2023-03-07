using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComUIElemCharInfo : ComUIElement
{
    [SerializeField] Image hpSprite;
    public Image HpSprite => hpSprite;

    public override void Init()
    {
        base.Init();

        actor = Managers.Object.MyActor;
        RegisterUIAction(CheckCharInfo);
    }

    private void CheckCharInfo()
    {
        hpSprite.fillAmount = actor.StatusAgent.HpPer;
    }
}
