using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddATK : BaseBuff
{
    protected override void Active()
    {
        Debug.Log("���ݷ� ����");
    }

    protected override void DeActive()
    {
        Debug.Log("���ݷ� ���� ��");
    }
}

public class AddDEF : BaseBuff
{
    protected override void Active()
    {
        Debug.Log("���� ����");
    }

    protected override void DeActive()
    {
        Debug.Log("���� ���� ��");
    }
}