using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction 
{
    private float nowCoolTime = 0;

    public bool IsCool = false;

    public virtual void Init()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void CalcCoolTime()
    {

    }


}
