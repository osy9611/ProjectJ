using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager 
{
    int nowSkillId;
    Dictionary<int,BaseAction> actions;
    private SkillAgent agent;

    public void Init(SkillAgent agent)
    {
        this.agent = agent;
        actions = new Dictionary<int, BaseAction>();
    }

    public void Execte()
    {
        if (nowSkillId == -1)
            return;

        actions[nowSkillId].Execute();
    }

    public void Register(int skillId)
    {
        
    }

}
