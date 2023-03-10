using Module.Unity.Pivot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAgent
{
    private Dictionary<int, GameObject> effects;

    public void Init(ComPivotAgent comPivotAgent, ActionManager actionManager)
    {
        if (comPivotAgent == null)
            return;

        if (effects == null)
            effects = new Dictionary<int, GameObject>();

        if (comPivotAgent.Pivots == null)
            return;

        foreach (var info in comPivotAgent.Pivots)
        {
            if (info.Type == PivotType.Effect)
            {
                BaseAction action = actionManager.GetAction(info.Id);
                if (actionManager.GetAction(info.Id) == null)
                    continue;

                if (action.SkillInfo.effect_Id_ref.effect_path != "")
                    Init(info, action.SkillInfo.effect_Id_ref.effect_path);
                Managers.Effect.Create(action.SkillInfo.hit_effect_Id,true);
            }
        }
    }

    private void Init(PivotInfo info, string path)
    {
        if (effects.ContainsKey(info.Id))
            return;

        GameObject obj = Managers.Effect.Get(path, info.PivotTr,1);
        if (obj == null)
            return;
        obj.transform.localPosition = info.PositionOffset;
        obj.transform.localRotation = Quaternion.Euler(info.RotationOffset);
        obj.transform.localScale = info.ScaleOffset;

        obj.SetActive(false);

        effects.Add(info.Id, obj);
    }

    public void SetActiveEffect(int id)
    {
        if (effects.ContainsKey(id))
        {
            effects[id].SetActive(!effects[id].activeSelf);
        }
    }
}
