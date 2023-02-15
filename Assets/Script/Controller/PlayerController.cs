using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : Controller
{
    private QViewControl qViewController;
    private bool isMove;
    public bool IsMove { get => isMove; }

    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        PlayerActor playerActor = actor as PlayerActor;
        if (playerActor == null)
            return;

        speed = playerActor.UserInfo.char_moveSpeed;
        qViewController = new QViewControl();
        qViewController.Init(actor.Creature as ComPlayerActor, 10);
        Managers.Input.RegisterInput(actor.Creature as ComPlayerActor, "PC");
        RegisterFunc();
    }

    public override void Execute()
    {
        Move();
    }

    public override void LateExecute()
    {

    }

    public override void Resset()
    {

    }

    protected override void Move()
    {
        if (Managers.Input.GetNowContorolScheme() == "PC")
        {
            if (!IsMove)
                return;

            SetDir();
        }
        qViewController.Execute();
    }

    public void SetDir()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == null)
                return;
            Vector2 nowPos = new Vector2(actor.Creature.transform.position.x, actor.Creature.transform.position.z);
            Vector2 hitPoint = new Vector2(hit.point.x, hit.point.z);

            Vector2 dir = hitPoint - nowPos;
            qViewController.Move(dir.normalized);
        }
        actor.FSM.ChangeState(Define.ObjectState.Move);
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
            Managers.Input.AddEvent("Skill", OnSkill, Define.InputEvnetType.Start | Define.InputEvnetType.Cancel);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isMove = false;
            actor.FSM.ChangeState(Define.ObjectState.Idle);
        }            
        else
        {
            isMove = true;
            actor.FSM.ChangeState(Define.ObjectState.Move);
        }

        if (Managers.Input.GetNowContorolScheme() == "Mobile")
        {
            qViewController.Move(context.ReadValue<Vector2>());
        }
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        if (context.canceled)
            return;

        switch (context.control.name)
        {
            case "leftButton":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillAttackType.NormalAttack1);
                break;
            case "q":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillAttackType.Skill1);
                break;
            case "w":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillAttackType.Skill2);
                break;
            case "e":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillAttackType.Skill3);
                break;
            case "r":
                actor.SkillAgent.OnSkill((int)DesignEnum.SkillAttackType.Skill4);
                break;
        }    
    }
}
