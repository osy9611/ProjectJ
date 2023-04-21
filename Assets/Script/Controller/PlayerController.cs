using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Module.Unity.Input;
using UnityEngine.EventSystems;
using Module.Core.Systems.Events;

public class PlayerController : Controller
{
    private QViewControl qViewController;
    public QViewControl QViewController { get => qViewController; }
    private bool isMove;
    public bool IsMove { get => isMove; }

    private bool isOverUI = false;

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

        actor.EventEmmiter.AddListener(checkPointerOverGameObject);
    }


    public void RegisterFunc()
    {
        if (Managers.Input.NowControllScheme == "Mobile")
        {
            Managers.Input.AddEvent("MoveAxis", OnMove, InputEvnetType.Performed | InputEvnetType.Cancel);
        }
        else
        {
            Managers.Input.AddEvent("Move", OnMove, InputEvnetType.Start | InputEvnetType.Cancel);
        }
        Managers.Input.AddEvent("Skill", OnSkill, InputEvnetType.Start | InputEvnetType.Cancel);
        Managers.Input.AddEvent("Interaction", OnInteractive, InputEvnetType.Start | InputEvnetType.Cancel);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (Managers.Input.NowControllScheme == "Mobile")
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

    private void checkPointerOverGameObject()
    {
        isOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        if (context.canceled)
            return;

        if (Managers.Input.NowControllScheme == "PC")
        {
            if (isOverUI)
                return;
        }

        switch (context.control.name)
        {
            case "leftButton":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillID.Attack);
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

    private void OnInteractive(InputAction.CallbackContext context)
    {
        if (context.canceled)
            return;

        PlayerActor playerActor = actor as PlayerActor;
        if (playerActor == null)
            return;

        if (playerActor.InteractiveActors.Count == 0)
            return;

        playerActor.InteractiveActors.OrderBy(v => v.gameObject.transform);

        switch (context.control.name)
        {
            case "f":
                playerActor.InteractiveActors[0].Interactive();
                break;
        }
    }
}
