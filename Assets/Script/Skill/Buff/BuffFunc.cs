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
        base.DeActive();
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
        base.DeActive();
        Debug.Log("���� ���� ��");
    }
}

public class LowATK:BaseBuff
{
    protected override void Active() 
    {
        Debug.Log("���ݷ� ����");
    }
    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("���ݷ� ���� ��");
    }
}

public class LowDEF : BaseBuff
{
    protected override void Active() 
    {
        Debug.Log("���� ����");
    }
    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("���� ���� ��");
    }
}

public class Strun : BaseBuff
{
    protected override void Active() 
    {
        Debug.Log("����");
    }

    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("���� ��");
    }
}

public class Dot : BaseBuff
{
    protected override void Active()
    {
        Debug.Log("��Ʈ");
    }

    protected override void DeActive()
    {
        base.DeActive();
        Debug.Log("��Ʈ ��");
    }
}