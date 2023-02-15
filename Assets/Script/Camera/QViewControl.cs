using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QViewControl
{
    private float moveSpeed;
    private float rotateSpeed;

    private ComQViewCamera comQViewCamera;
    private ComPlayerActor target;
    private PlayerController controller;

    private Vector2 dir;
    private Vector3 moveDir;
    private Quaternion targetQuat;
    public Quaternion TargetQuat => targetQuat;

    private Vector3 targetForward;
    public Vector3 TargetForward => targetForward;

    private bool isMove = false;

    public void Init(ComPlayerActor target, float rotateSpeed)
    {
        this.rotateSpeed = rotateSpeed;
        SetCamera();
        SetTarget(target);
    }

    public void Execute()
    {
        if (comQViewCamera == null || target == null)
            return;

        if (controller.IsMove || dir ==Vector2.zero)
            MoveInternal();

        target.Actor.CharCon.Move(new Vector3(moveDir.x, -1, moveDir.z) * Time.deltaTime * moveSpeed);
    }

    public void SetCamera()
    {
        this.comQViewCamera = Camera.main.GetComponent<ComQViewCamera>();
        isMove = false;
    }

    public void SetTarget(ComPlayerActor target)
    {
        comQViewCamera.SetTarget(target.transform);
        this.target = target;
        targetQuat = this.target.transform.rotation;
        isMove = false;
        controller = target.Actor.Controller as PlayerController;
        moveSpeed = 10;
    }

    public void Move(Vector2 dir)
    {
        this.dir = dir;
    }


    private void MoveInternal()
    {
        float delta = Time.deltaTime * moveSpeed;
        if (dir.sqrMagnitude < 0.01)
        {
            if (isMove)
            {
                isMove = false;
            }
            moveDir = Vector3.zero;
            return;
        }

        if (!isMove)
            isMove = true;


        moveDir = Quaternion.Euler(0, comQViewCamera.Camera.transform.eulerAngles.y, 0) * new Vector3(this.dir.x, 0, this.dir.y);
        moveDir.Normalize();
        target.Actor.Dir = new Vector2(moveDir.x, moveDir.z);
        target.Actor.Creature.transform.rotation = Quaternion.LookRotation(new Vector3(moveDir.x, 0.0f, moveDir.z), Vector3.up);
    }

    protected void UpdateRotate()
    {
        target.transform.rotation = Quaternion.Lerp(target.transform.rotation, TargetQuat, rotateSpeed * Time.deltaTime);
    }
}
