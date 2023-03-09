using Module.Unity.Pivot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComPivotAgent))]
public abstract class ComBaseActor : MonoBehaviour
{
    protected BaseActor actor;
    public BaseActor Actor { get => actor; set=>actor=value; }
    protected ComPivotAgent pivotAgent;

    public ComPivotAgent PivotAget=>pivotAgent;

    protected ComHudUnitInfo hudUnitInfo;

    public ComHudUnitInfo HudUnitInfo=>hudUnitInfo;


    private void Awake()
    {
        pivotAgent = GetComponent<ComPivotAgent>();
    }

    private void Start()
    {

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

    protected virtual void UpdateComActor()
    {
        if (actor == null)
            return;

        actor.UpdateActor();
    }

    protected virtual void LateUpdateComActor()
    {
        if (actor == null)
            return;

        actor.LateUpdateActor();
        hudUnitInfo.Execute();
    }

    public virtual void OnEffect(int id)
    {
        actor.SkillAgent.OnEffect(id);
    }

    public virtual void OnJudge()
    {
        actor.SkillAgent.OnJudge();
    }
}
