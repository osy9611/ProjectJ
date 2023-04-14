using Module.Core.Systems.Events;
using Module.Unity.DayNight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComBattleScene : ComScene
{
    ComDayNight dayNight;
    protected override void Start()
    {
        base.Start();
        dayNight = FindObjectOfType<ComDayNight>();

        if(dayNight != null)
        {
            InitEvent();
        }
    }

    private void InitEvent()
    {
        dayNight.OnNight.AddListener(ShowText);
    }

    public void ShowText()
    {
        Managers.UI.ShowNotification(0, new Args<string>("Æ÷Å» ¿­¸²"));
    }

}
