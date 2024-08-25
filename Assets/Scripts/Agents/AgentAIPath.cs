using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class AgentAIPath : AIPath
{
    public new event Action<string> OnTargetReached;

    [SerializeField]
    private Agent agent;
    protected override void Awake()
    {
        base.Awake();
        agent = this.GetComponent<Agent>();
    }
    protected override void OnPathComplete(Path p)
    {
        base.OnPathComplete(p);
        if (agent!=null && !p.error && IsReachedDestination())
        {
            OnDestinationReached();
        }
    }

    void OnDestinationReached()
    {
        OnTargetReached?.Invoke(agent.GUID);
        agent.SetNewRandomDestination();
    }

    bool IsReachedDestination()
    {
        return Vector3.Distance(transform.position, destination) < endReachedDistance;
    }
}
