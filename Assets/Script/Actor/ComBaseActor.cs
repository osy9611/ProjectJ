using Module.Unity.Pivot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComPivotAgent))]
public abstract class ComBaseActor : MonoBehaviour
{
    protected BaseActor actor;
    public BaseActor Actor { get => actor; set => actor = value; }
    protected ComPivotAgent pivotAgent;

    public ComPivotAgent PivotAget => pivotAgent;

    protected ComHudUnitInfo hudUnitInfo;

    public ComHudUnitInfo HudUnitInfo => hudUnitInfo;


    protected virtual void Awake()
    {
        pivotAgent = GetComponent<ComPivotAgent>();
    }

    protected virtual void Start()
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

    private void OnEnable()
    {
        Enable();   
    }

    private void OnDisable()
    {
        Disable();
    }

    public abstract void Init();

    protected virtual void UpdateComActor()
    {
        if (actor != null)
            actor.UpdateActor();
    }

    protected virtual void LateUpdateComActor()
    {
        if (actor != null)
            actor.LateUpdateActor();

        if (hudUnitInfo != null)
            hudUnitInfo.Execute();
    }

    protected virtual void Enable()
    {
        if (actor != null)
            actor.Enable();

        if(hudUnitInfo !=null)
            hudUnitInfo.Init(pivotAgent, actor);
    }

    protected virtual void Disable()
    {
        if(actor != null)
            actor.Disable();
        if(hudUnitInfo != null)
            hudUnitInfo.Release();
    }

    public virtual void OnEffect(int id)
    {
        actor.SkillAgent.OnEffect(id);
    }

    public virtual void OnJudge()
    {
        actor.SkillAgent.OnJudge();
    }

    public virtual void OnSound()
    {
        actor.SkillAgent.OnSound();
    }

}
