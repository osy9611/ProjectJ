using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComStart : MonoBehaviour
{
    public void StartScene()
    {
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }
}
