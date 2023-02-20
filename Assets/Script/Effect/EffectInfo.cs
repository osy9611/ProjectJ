using Module.Unity.Pivot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo
{
    private Dictionary<int, GameObject> effects;

    public void Init(ComPivotAgent comPivotAgent, ActionManager actionManager)
    {
        if (comPivotAgent == null)
            return;

        if (effects == null)
            effects = new Dictionary<int, GameObject>();

        foreach (var info in comPivotAgent.Pivots)
        {
            if (info.Type == PivotType.Effect)
            {
                BaseAction action = actionManager.GetAction(info.Id);
                if (actionManager.GetAction(info.Id) == null)
                    continue;

                Debug.Log(action.SkillInfo.effect_Id_ref.effect_path);
                if (action.SkillInfo.effect_Id_ref.effect_path != "")
                    Init(info, action.SkillInfo.effect_Id_ref.effect_path);
            }
        }
    }

    public void Init(PivotInfo info, string path)
    {
        if (effects.ContainsKey(info.Id))
            return;

        GameObject obj = Managers.Resource.LoadAndPool(path, info.PivotTr);
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
