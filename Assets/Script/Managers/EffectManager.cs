using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EffectManager
{
    public void Init()
    {
    }

    public void Create(int id, bool addComEffect = false)
    {
        var info = Managers.Data.Skill_effectInfos.Get(id);

        if (info == null)
            return;       
        if(addComEffect)
        {
            Managers.Resource.LoadAndCreate<ComEffect>(info.effect_path, null, 5);
        }
        else
        {
            Managers.Resource.LoadAndCreate(info.effect_path, null, 5);
        }
    }

    public void Get(int id, Vector3 pos)
    {
        var info = Managers.Data.Skill_effectInfos.Get(id);

        if (info == null)
            return;
        GameObject obj = Managers.Resource.LoadAndPool(info.effect_path,null);
        obj.transform.position = pos;
        
    }

    public GameObject Get(string path, Transform tr, int count)
    {
        return Managers.Resource.LoadAndPool(path, tr, count);
    }

    public void Destroy(GameObject go, bool destroyPool = false)
    {
        Managers.Resource.Destory(go, destroyPool);
    }
}
