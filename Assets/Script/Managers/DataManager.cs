using DesignTable;
using Module.Unity.Core;
using ProjectJ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : DesignTable.DataMgr
{
    ComDataAssets dataAssets = null;
    public ComDataAssets DataAssets { get => dataAssets; }

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
        yield return Managers.Resource.CoLoadAsset<GameObject>(Define.tableDataAssetPath,
           (resAsset) =>
           {
               dataAssets = resAsset.GetComponent<ComDataAssets>();
               dataAssets.Init();
           });


        foreach (var asset in dataAssets.TableAsset.Datas)
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
