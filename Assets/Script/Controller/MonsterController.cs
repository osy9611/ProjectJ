using Module.Core.Systems.Events;
using Module.Unity.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : Controller
{
    ComPathAgent pathAgent;
    NavMeshAgent navAgent;
    private int pathIndex;
    private bool revers;
    private bool reachPath;

    private float keepRange;
    private float originSearchRange;
    private float maxSearchRange;

    private BaseActor target;
    private EventArgs<float, float> checkSearchArgs;

    public bool ReachPath { get => reachPath; set => reachPath = value; }

    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        MonsterActor monsterActor = actor as MonsterActor;
        if (monsterActor == null)
            return;
        pathAgent = actor.Creature.GetComponent<ComPathAgent>();
        navAgent = actor.Creature.GetComponent<NavMeshAgent>();
        actor.Creature.transform.position = pathAgent.PathData[0];

        if(navAgent !=null)
        {
            navAgent.speed = monsterActor.MonsterInfo.mon_speed;
        }

        originSearchRange = monsterActor.MonsterInfo.mon_searchRange;
        maxSearchRange = monsterActor.MonsterInfo.mon_searchRange * 2.0f;
        keepRange = monsterActor.MonsterInfo.mon_keppRange;
        checkSearchArgs = new EventArgs<float, float>(originSearchRange, monsterActor.MonsterInfo.mon_searchAngle);
    }

    public void MovePath()
    {
        if (pathAgent == null || navAgent == null)
            return;

        if (pathAgent.PathData == null)
            return;

        SearchTarget();
        Vector3? nextPos = null;
        if (target == null)
        {
            nextPos = pathAgent.GetNextPath(pathIndex);
            if (nextPos == null)
            {
                if (pathAgent.PathData.Count - 1 < pathIndex)
                {
                    revers = true;
                    pathIndex = pathAgent.PathData.Count - 1;
                }

                if (0 > pathIndex)
                {
                    revers = false;
                    pathIndex = 0;
                }
                reachPath = true;
                return;
            }            
        }
        else
        {
            nextPos = target.Creature.transform.position;
        }        

        Vector2 targetPath = new Vector2(((Vector3)nextPos).x, ((Vector3)nextPos).z);

        if (CheckKeepRange(targetPath,0.2f))
        {
            if (target == null)
            {
                if (!revers)
                    pathIndex++;
                else
                    pathIndex--;
            }
        }
        else
        {
            if(navAgent.enabled)
                navAgent.SetDestination((Vector3)nextPos);
        }
    }

    public bool SearchTarget()
    {
        target = Managers.Judge.CheckTarget(actor, checkSearchArgs);
        if(target == null)
        {
            checkSearchArgs.Arg1 = originSearchRange;
            return false;
        }

        checkSearchArgs.Arg1 = maxSearchRange;
        return true;
    }

    public bool CheckAttackRange()
    {
        if (target == null)
            return false;

        Vector2 targetPath = new Vector2(target.Creature.transform.position.x, target.Creature.transform.position.z);

        if (CheckKeepRange(targetPath, keepRange))
        {
            navAgent.enabled = false;
            return true;
        }
        else
            navAgent.enabled = true;

        return false;
    }

    private bool CheckKeepRange(Vector2 target, float range)
    {
        Vector2 nowPos = new Vector2(actor.Creature.transform.position.x, actor.Creature.transform.position.z);
        if(Vector2.Distance(nowPos,target) <= range)
        {
            return true;
        }
        return false;
    }
}
