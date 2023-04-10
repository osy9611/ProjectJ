using Module.Core.Systems.Events;
using Module.Unity.Custermization;
using Module.Unity.UGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComUIElemCostume : UI_Base
{
    enum PartColors
    {
        HeadColor,
        ClothColor,
        FootColor
    }

    private ComCostumeMode costumeMode;
    public override void Init()
    {
        Bind<Image>(typeof(PartColors));
        costumeMode = Managers.UI.GetScene<ComCostumeMode>();

        if (costumeMode == null)
            return;

        int partLength = System.Enum.GetNames(typeof(Define.CostumePart)).Length;

        for(int i=0;i<partLength;++i)
        {
            Color color = (Color)costumeMode.GetColr((Define.CostumePart)i, 0);
            if (color == null)
                continue;
            Get<Image>(i).color = color;
        }
    }

    public void ChangeUp(int id)
    {
        Change(id, true);
    }

    public void ChangeDown(int id)
    {
        Change(id, false);
    }

    private void Change(int id, bool isUp)
    {
        if (costumeMode == null || Get<Image>(id) == null)
            return;

        Color? color = costumeMode.GetColor((Define.CostumePart)id, isUp);
        if (color == null || Get<Image>(id) == null)
            return;

        Get<Image>(id).color = (Color)color;

        if (costumeMode.Agent == null)
            return;

        if(id != 0)
        {
            id += 1;
        }

        costumeMode.ChangeCostumeColor(0, (Color)color, new EventArgs<int>(id));
    }

}
