using Module.Unity.UGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComBattleMode : UI_Scene
{
    private ComManagedSpriteAtlas managedSpriteAtlas;

    public override void Init()
    {
        base.Init();
        GameObject spriteAtlas = Managers.Resource.LoadAndPool(Define.CommonAtlasPath, null, 1);
        managedSpriteAtlas = spriteAtlas.GetComponent<ComManagedSpriteAtlas>();
    }

}
