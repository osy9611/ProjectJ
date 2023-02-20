using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddATK : BaseBuff
{
    protected override void Active()
    {
        actor.StatusAgent.IncreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Str, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }

    protected override void DeActive()
    {
        base.DeActive();
        actor.StatusAgent.DecreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Str, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }
}
public class AddDEF : BaseBuff
{
    protected override void Active()
    {
        actor.StatusAgent.IncreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Dex, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);

    }

    protected override void DeActive()
    {
        base.DeActive();
        actor.StatusAgent.DecreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Dex, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }
}

public class LowATK:BaseBuff
{
    protected override void Active() 
    {
        actor.StatusAgent.DecreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Str, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }
    protected override void DeActive()
    {
        base.DeActive();
        actor.StatusAgent.IncreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Str, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }
}
public class LowDEF : BaseBuff
{
    protected override void Active()
    {
        actor.StatusAgent.DecreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Dex, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }
    protected override void DeActive()
    {
        base.DeActive();
        actor.StatusAgent.IncreaseStatus(StatusDefine.StatusGroupId.Buff, DesignEnum.AttributeId.Dex, (long)buffInfo.buff_arg1, buffInfo.buff_usePercent);
    }
}

    public class Strun : BaseBuff
{
    protected override void Active() 
    {
        Managers.Ani.SetEnableAni(actor.Ani, false);
    }

    protected override void DeActive()
    {
        base.DeActive();
        Managers.Ani.SetEnableAni(actor.Ani, true);
    }
}

public class Dot : BaseBuff
{
    protected override void Active()
    {
        actor.StatusAgent.DecreaseHP(StatusDefine.HPType.NowHP, (long)buffInfo.buff_arg1);
    }

    protected override void DeActive()
    {
        base.DeActive();
    }
}