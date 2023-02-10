using DesignTable;
using Module.Automation;
using Module.Unity.Core;
using ProjectJ;
using ProjectJ.ClientVariable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class DataManager : DesignTable.DataMgr
{
    private ComClientVarable clientVariable;
    public ComClientVarable ClientVariable { get => clientVariable; }


    public override void Init()
    {
        base.Init();
        ComLoader.s_Root.StartCoroutine(LoadData());
        
    }

    IEnumerator LoadData()
    {
        ComTableAsset tableAsset = null;
        yield return Managers.Resource.CoLoadAsset<GameObject>(Define.tableDataAssetPath,
           (resAsset) =>
           {
               ComDataAssets dataAssets = resAsset.GetComponent<ComDataAssets>();
               clientVariable = dataAssets.ClientVarable;
               tableAsset = dataAssets.TableAsset;
           });

        if (tableAsset != null)
        {
            yield return ComLoader.s_Root.StartCoroutine(LoadTablesData(tableAsset));
            base.SetUpRef();
        }

        Managers.Resource.Release(Define.tableDataAssetPath);
    }


    IEnumerator LoadTablesData(ComTableAsset tableAsset)
    {
        foreach (var asset in tableAsset.Datas)
        {
            string talbeName = TextDefine.GetTalbePath(asset.Addressable);
            TableId id = (TableId)asset.TableId;
            yield return Managers.Resource.CoLoadAsset<TextAsset>(talbeName,
                (resAsset)=>
                {
                    if(resAsset== null)
                    {
                        Debug.Log(talbeName);
                        return;
                    }
                    base.LoadData(id, resAsset.bytes);
                });

            Managers.Resource.Release(talbeName);
        }


    }

    
}
