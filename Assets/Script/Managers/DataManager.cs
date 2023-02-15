using DesignTable;
using Module.Automation;
using Module.Unity.Core;
using ProjectJ;
using ProjectJ.ClientVariable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : DesignTable.DataMgr
{
    private ComClientVarable clientVariable;
    public ComClientVarable ClientVariable { get => clientVariable; }


    public override void Init()
    {
        base.Init();
    }

    public void OnLoadData(System.Action<bool> callback = null)
    {
        ComLoader.s_Root.StartCoroutine(CoLoadData(callback));
    }

    public IEnumerator CoLoadData(System.Action<bool> callback = null)
    {
        ComTableAsset tableAsset = null;
        yield return Managers.Resource.CoLoadAsset<GameObject>(Define.tableDataAssetPath,
           (resAsset) =>
           {
               ComDataAssets dataAssets = resAsset.GetComponent<ComDataAssets>();
               clientVariable = dataAssets.ClientVarable;
               tableAsset = dataAssets.TableAsset;
           });


        foreach (var asset in tableAsset.Datas)
        {
            string talbeName = TextDefine.GetTalbePath(asset.Addressable);
            TableId id = (TableId)asset.TableId;
            TextAsset textAsset = null;

            yield return Managers.Resource.CoLoadAsset<TextAsset>(talbeName,
                (resAsset) =>
                {
                    if (resAsset == null)
                    {
                        Debug.Log(talbeName);
                        return;
                    }

                    textAsset = resAsset;
                });

            base.LoadData(id, textAsset.bytes);
            Managers.Resource.Release(talbeName);
        }
        base.SetUpRef();
        Managers.Resource.Release(Define.tableDataAssetPath);
        callback?.Invoke(true);
    }


    
}
