using Module.Core.Systems.Collections.Generic;
using Module.Unity.Pivot;
using Module.Unity.UGUI.Hud;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComHudUnitInfo
{
    BaseActor actor;
    public ComHealthBar comHealthBar;
    private PivotInfo damagePivotInfo;
    private UnorderedList<ComDamageText> comDamageTexts = new UnorderedList<ComDamageText>();
    public void Init(ComPivotAgent comPivotAgent, BaseActor actor)
    {
        this.actor = actor;
        foreach (var info in comPivotAgent.Pivots)
        {
            if (info == null)
                continue;

            if (info.Type == PivotType.UI)
            {
                switch ((HudDefine.HudType)info.Id)
                {
                    case HudDefine.HudType.HPBar:
                        comHealthBar = Managers.Hud.Get<ComHealthBar>("Assets/Res/UI/Prefab/SubItem/Hud_HP.prefab", info);
                        break;
                    case HudDefine.HudType.Damage:
                        damagePivotInfo = info;
                        break;
                }

            }
        }

    }

    public void Execute()
    {
        if (comHealthBar != null)
        {
            comHealthBar.Execute();
            comHealthBar.SetHP(actor.StatusAgent.HpPer);
        }

        for(int i=0;i<comDamageTexts.Count;++i)
        {
            if (!comDamageTexts[i].gameObject.activeSelf)
            {
                comDamageTexts.RemoveAt(i);
                break;
            }
            comDamageTexts[i].Execute();
        }
    }

    public void SetDamage(float damage)
    {
        ComDamageText damageText = Managers.Hud.GetAndPool<ComDamageText>("Assets/Res/UI/Prefab/SubItem/DamgeText.prefab", damagePivotInfo);
        if(damageText.onDestoy ==null)
            damageText.onDestoy = Managers.Resource.Destory;

        damageText.DamageText = damage <= 0? "Miss" : damage.ToString();
        comDamageTexts.Add(damageText);
    }


}
