using Module.Unity.UGUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComUIElemSkill : ComUIBattleElement
{
    enum Images
    {
        Attack,
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill1_CoolTime,
        Skill2_CoolTime,
        Skill3_CoolTime,
        Skill4_CoolTime
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        List<string> resources = new List<string>();
        for (int i = (int)DesignEnum.SkillID.Attack1; i < (int)DesignEnum.SkillID.Skill4 + 1; ++i)
        {
            BaseAction action = Managers.Object.MyActor.SkillAgent.ActionManager.GetAction(i);
            if (action == null)
                continue;
            resources.Add(action.SkillInfo.image_Res);
        }

        for (int i = 0, range = (int)Images.Skill4 + 1; i < range; ++i)
        {
            Get<Image>(i).sprite = Managers.Atlas.Get(resources[i] + "Mobile");
        }

        RegisterUIAction(CheckSkillCoolTimeUI);
    }

    public void CheckSkillCoolTimeUI()
    {
        int start = (int)DesignEnum.SkillID.Skill1;
        int end = (int)DesignEnum.SkillID.Skill4 + 1;

        for (int i = start; i < end; ++i)
        {
            float coolTime = actor.SkillAgent.ActionManager.GetCoolTime(i);

            if (coolTime == -1)
                continue;
            SetActiveCoolTimeSprite(i, coolTime);
        }
    }

    private void SetActiveCoolTimeSprite(int id, float coolTime)
    {
        if ((int)DesignEnum.SkillID.Skill1 <= id && (int)DesignEnum.SkillID.Skill4 >= id)
        {
            int index = id - (int)DesignEnum.SkillID.Skill1;
            Image image = null;

            switch ((DesignEnum.SkillID)id)
            {
                case DesignEnum.SkillID.Skill1:
                    image = Get<Image>((int)Images.Skill1_CoolTime);
                    break;
                case DesignEnum.SkillID.Skill2:
                    image = Get<Image>((int)Images.Skill2_CoolTime);
                    break;
                case DesignEnum.SkillID.Skill3:
                    image = Get<Image>((int)Images.Skill3_CoolTime);
                    break;
                case DesignEnum.SkillID.Skill4:
                    image = Get<Image>((int)Images.Skill4_CoolTime);
                    break;
            }

            if (image == null)
                return;

            if (coolTime == 0)
            {
                image.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(true);
                image.fillAmount = 1 - coolTime;
            }
        }
    }

}
