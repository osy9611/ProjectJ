using Module.Unity.UGUI.Hud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComHealthBar : ComHudAgent
{
    [SerializeField] Image HpBar;

    public override void Execute()
    {
        base.Execute();
    }

    public void SetHP(float hp)
    {
        HpBar.fillAmount = hp;
    }
}
