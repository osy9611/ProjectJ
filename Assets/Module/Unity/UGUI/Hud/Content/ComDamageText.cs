using Module.Core.Systems.Collections.Generic;
using Module.Unity.Core;
using Module.Unity.Pivot;
using Module.Unity.UGUI.Hud;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComDamageText : ComHudAgent
{
    [SerializeField] float moveSpeed;
    [SerializeField] float alphaSpeed;
    [SerializeField] float destroyTime;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Color alpha;

    public override void Init(PivotInfo pivotInfo)
    {
        base.Init(pivotInfo);

        Invoke("Destroy",destroyTime);
    }

    public override void Execute()
    {
        base.Execute();
        UpdateText();
        
    }

    public void UpdateText()
    {
        text.transform.Translate(new Vector3(0, Time.deltaTime * moveSpeed, 0));
        alpha.a = Mathf.Lerp(text.color.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;

    }

    protected override void Destroy()
    {
        alpha.a = 1;
        base.Destroy();
    }

}
