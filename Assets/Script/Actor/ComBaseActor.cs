using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComBaseActor : MonoBehaviour
{
    protected BaseActor actor;
    public BaseActor Actor { get => actor; set=>actor=value; }

    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateComActor();
    }

    private void LateUpdate()
    {
        LateUpdateComActor();
    }

    public abstract void Init();

    protected abstract void UpdateComActor();

    protected abstract void LateUpdateComActor();
}
