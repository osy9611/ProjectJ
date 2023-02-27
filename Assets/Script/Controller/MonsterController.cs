using Module.Core.Systems.Events;
using Module.Unity.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : Controller
{
    PathInfo pathInfo;
    NavMeshAgent navAgent;
    private int pathIndex;
    private bool revers;
    private bool reachTarget;

    private float keepRange;
    private float originSearchRange;
    private float maxSearchRange;

    private BaseActor target;
    private EventArgs<float, float> checkSearchArgs;

    public bool ReachTarget { get => reachTarget; }

    private bool pathMove;
    public bool PathMove { get => pathMove; }

    public override void Init(BaseActor actor)
    {
        base.Init(actor);

        MonsterActor monsterActor = actor as MonsterActor;
        if (monsterActor == null)
            return;
        pathInfo = actor.Creature.GetComponent<ComPathAgent>().GetPath(0);
        navAgent = actor.Creature.GetComponent<NavMeshAgent>();

        pathMove = pathInfo.PathData.Count > 1;
        actor.Creature.transform.position = pathInfo.PathData[0];


        if (navAgent != null)
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
        if (navAgent == null)
            return;

        SearchTarget();
        Vector3? nextPos = null;
        if (target == null)
        {
            if (pathInfo == null)
                return;

            if (pathInfo.PathData.Count <= 1)
            {
                nextPos = pathInfo.GetPath(0);
                if(CheckKeepRange(new Vector2(((Vector3)nextPos).x, ((Vector3)nextPos).z),0.2f))
                {
                    reachTarget = true;
                    return;
                }
                reachTarget = false;
            }
            else
            {
                nextPos = pathInfo.GetPath(pathIndex);

                if (nextPos == null)
                {
                    if (pathInfo.PathData.Count - 1 < pathIndex)
                    {
                        revers = true;
                        pathIndex = pathInfo.PathData.Count - 1;
                    }

                    if (0 > pathIndex)
                    {
                        revers = false;
                        pathIndex = 0;
                    }
                    reachTarget = true;
                    return;
                }
                else
                {
                    reachTarget = false;
                }
            }
        }
        else
        {
            if (CheckAttackRange())
            {
                reachTarget = true;
                return;
            }
            else
            {
                reachTarget = false;
                nextPos = target.Creature.transform.position;
            }
        }

        Vector2 targetPath = new Vector2(((Vector3)nextPos).x, ((Vector3)nextPos).z);

        if (CheckKeepRange(targetPath, 0.2f))
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
            if (navAgent.enabled)
                navAgent.SetDestination((Vector3)nextPos);
        }
    }

    public bool SearchTarget()
    {
        target = Managers.Judge.CheckTarget(actor, checkSearchArgs);
        if (target == null)
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
            reachTarget = true;
            return true;
        }
        else
            navAgent.enabled = true;

        return false;
    }

    private bool CheckKeepRange(Vector2 target, float range)
    {
        Vector2 nowPos = new Vector2(actor.Creature.transform.position.x, actor.Creature.transform.position.z);
        if (Vector2.Distance(nowPos, target) <= range)
        {
            return true;
        }
        return false;
    }
}
