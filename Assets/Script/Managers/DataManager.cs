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
            //if (asset.Addressable == "skill.bytes")
            //    TestCode(textAsset.bytes);
            base.LoadData(id, textAsset.bytes);
            Managers.Resource.Release(talbeName);
        }
        base.SetUpRef();
        Managers.Resource.Release(Define.tableDataAssetPath);
        callback?.Invoke(true);
    }

    //DataMessageSerializer serializer = new DataMessageSerializer();
    //private void TestCode(byte[] bytes)
    //{
    //    using (new MemoryStream(bytes))
    //    {
    //        skillInfos dataInfo = serializer.Deserialize(1013, bytes) as skillInfos;
    //        //dataInfo.Initialize();

    //        foreach (skillInfo item in dataInfo.dataInfo)
    //        {
    //            ArraySegment<byte> idRule = dataInfo.GetIdRule(item.unit_Class, item.unit_type,item.skill_Id);
    //            if (!dataInfo.datas.ContainsKey(idRule))
    //            {
    //                dataInfo.datas.Add(idRule, new skillInfo(item.unit_Class, item.unit_type, item.skill_Id, item.skill_coolTime, item.skill_range, item.skill_radius, item.skill_scale, item.skill_buffId, item.skill_type, item.skill_attackType, item.skill_contoroll, item.skill_dash, item.skill_dashSpeed, item.skill_judgeAni, item.skill_judgeTime, item.effect_Id));
    //                idRule = dataInfo.GetListIdRule(item.unit_Class, item.unit_type);
    //                if (dataInfo.listData.ContainsKey(idRule))
    //                {
    //                    dataInfo.listData[idRule].Add(item);
    //                    continue;
    //                }

    //                dataInfo.listData.Add(idRule, new List<skillInfo>());
    //                dataInfo.listData[idRule].Add(item);
    //            }
    //        }
    //    }
    //}
    
}
