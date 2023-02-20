using DesignTable;
using Module.Core.Systems.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseBuff
{
    private BuffManager buffManager;
    protected BaseActor actor;
    private float nowDurationTime = 0;
    private float nowInterval = 0;
    protected buffInfo buffInfo;

    private bool stopInterval = false;

    private bool isEnd = false;
    public bool IsEnd { get => isEnd; }

    public virtual void Init(BuffManager buffMangaer,IEventArgs arg)
    {
        this.buffManager = buffMangaer;
        //duration, interval
        EventArgs<BaseActor, buffInfo>? val = arg as EventArgs<BaseActor, buffInfo>?;
        if (!val.HasValue)
            return;
        actor = val.Value.Arg1;
        buffInfo = val.Value.Arg2;

        Debug.Log("버프 등록완료!");
    }

    public virtual void Execute()
    {
        if (!isEnd)
            CalcDurationTime();
    }

    protected virtual void CalcDurationTime()
    {
        if(nowDurationTime <= buffInfo.buff_duration)
        {
            nowDurationTime += Time.deltaTime;
            if(!stopInterval)
                CalcIntervalTime();
        }
        else
        {
            isEnd = true;
            DeActive();
        }
    }

    protected virtual void CalcIntervalTime()
    {
        if(buffInfo.buff_interval == 0)
        {
            stopInterval = true;
            Active();
            return;
        }

        if(nowInterval<= buffInfo.buff_interval)
        {
            nowInterval += Time.deltaTime;
        }
        else
        {
            Active();
            nowInterval = 0;
        }
    }

    protected abstract void Active();

    protected virtual void DeActive()
    {
        this.buffManager.UnRegister(this);
    }
}
