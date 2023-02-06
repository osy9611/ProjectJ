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
        Debug.Log("방어력 증가 끝");
    }
}