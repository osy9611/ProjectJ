using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerInput playerInput;
    private PlayerController controller;
    public void Init()
    {
    }

    public void RegisterInput(ComPlayerActor comPlayerActor, string schemeType)
    {
        this.playerInput = comPlayerActor.GetComponent<PlayerInput>();
        this.controller = comPlayerActor.Actor.Controller as PlayerController;

        playerInput.defaultControlScheme = schemeType;
    }

    public string GetNowContorolScheme()
    {
        return playerInput.defaultControlScheme;
    }

    public void AddEvent(string eventName, System.Action<InputAction.CallbackContext> callback, Define.InputEvnetType eventType)
    {
        if (callback == null)
            return;

        if (eventType.HasFlag(Define.InputEvnetType.Start))
            playerInput.actions[eventName].started += callback;
        if (eventType.HasFlag(Define.InputEvnetType.Performed))
            playerInput.actions[eventName].performed += callback;
        if (eventType.HasFlag(Define.InputEvnetType.Cancel))
            playerInput.actions[eventName].canceled += callback;
    }

    public void RemoveEvent(string eventName, System.Action<InputAction.CallbackContext> callback)
    {
        if (callback == null)
            return;
        playerInput.actions[eventName].performed -= callback;
    }


}

