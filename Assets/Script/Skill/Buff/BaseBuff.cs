using Module.Core.Systems.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseBuff
{
    private float nowDurationTime = 0;
    private float nowInterval = 0;
    private float durationTime = 0;
    private float interval = 0;

    private bool stopInterval = false;

    private bool isEnd = false;
    public bool IsEnd { get => isEnd; }
    BuffManager buffManager = null;

    public virtual void Init(IEventArgs arg,BuffManager buffManager)
    {
        //duration, interval
        EventArgs<float,float>? val = arg as EventArgs<float, float>?;
        if (!val.HasValue)
            return;

        durationTime = val.Value.Arg1;
        interval = val.Value.Arg2;

        Debug.Log("버프 등록완료!");
    }

    public virtual void Execute()
    {
        if (!isEnd)
            CalcDurationTime();
    }

    protected virtual void CalcDurationTime()
    {
        if(nowDurationTime <= durationTime)
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
        if(interval == 0)
        {
            stopInterval = true;
            Active();
            return;
        }

        if(nowInterval<= interval)
        {
            nowInterval += Time.deltaTime;
        }
        else
        {
            nowInterval = 0;
        }
    }

    protected abstract void Active();

    protected virtual void DeActive()
    {
    }
}
