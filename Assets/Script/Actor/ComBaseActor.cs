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

    private void Awake()
    {
        pivotAgent = GetComponent<ComPivotAgent>();
    }

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

    public virtual void OnEffect(int id)
    {
        actor.SkillAgent.OnEffect(id);
    }

    public virtual void OnJudge()
    {
        actor.SkillAgent.OnJudge();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position + transform.forward * 2, new Vector3(0.5f+3,0.5f,0.5f + 3));
    }

}
