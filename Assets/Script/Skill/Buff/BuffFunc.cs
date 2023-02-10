using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddATK : BaseBuff
{
    protected override void Active()
    {
        Debug.Log("공격력 증가");
    }

    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("공격력 증가 끝");
    }
}

public class AddDEF : BaseBuff
{
    protected override void Active()
    {
        Debug.Log("방어력 증가");
    }

    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("방어력 증가 끝");
    }
}

public class LowATK:BaseBuff
{
    protected override void Active() 
    {
        Debug.Log("공격력 감소");
    }
    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("공격력 감소 끝");
    }
}

public class LowDEF : BaseBuff
{
    protected override void Active() 
    {
        Debug.Log("방어력 감소");
    }
    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("방어력 감소 끝");
    }
}

public class Strun : BaseBuff
{
    protected override void Active() 
    {
        Debug.Log("스턴");
    }

    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("스턴 끝");
    }
}

public class Dot : BaseBuff
{
    protected override void Active()
    {
        Debug.Log("도트");
    }

    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("도트 끝");
    }
}