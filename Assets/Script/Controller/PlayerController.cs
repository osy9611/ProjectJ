using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : Controller
{
    private QViewControl qViewController;
    public QViewControl QViewController { get => qViewController; }
    private bool isMove;
    public bool IsMove { get => isMove;}

    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        PlayerActor playerActor = actor as PlayerActor;
        if (playerActor == null)
            return;

        speed = playerActor.UserInfo.char_moveSpeed;
        qViewController = new QViewControl();
        qViewController.Init(actor.Creature as ComPlayerActor, 10);
#if UNITY_ANDROID || UNITY_IOS
        Managers.Input.RegisterInput(actor.Creature as ComPlayerActor, "Mobile");
#else
    Managers.Input.RegisterInput(actor.Creature as ComPlayerActor, "PC");
#endif

        RegisterFunc();
    }


    public void RegisterFunc()
    {
        if (Managers.Input.GetNowContorolScheme() == "Mobile")
        {
            Managers.Input.AddEvent("MoveAxis", OnMove, Define.InputEvnetType.Performed | Define.InputEvnetType.Cancel);
        }
        else
        {
            Managers.Input.AddEvent("Move", OnMove, Define.InputEvnetType.Start | Define.InputEvnetType.Cancel);            
        }
        Managers.Input.AddEvent("Skill", OnSkill, Define.InputEvnetType.Start | Define.InputEvnetType.Cancel);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (Managers.Input.GetNowContorolScheme() == "Mobile")
        {
            qViewController.Move(context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        if (context.canceled)
            return;

        switch (context.control.name)
        {
            case "leftButton":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillID.NormalAttack1);
                break;
            case "q":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillID.Skill1);
                break;
            case "w":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillID.Skill2);
                break;
            case "e":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillID.Skill3);
                break;
            case "r":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillID.Skill4);
                break;
        }
    }
}
