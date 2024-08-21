using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentService
{
    event Action OnRequestAgentSpawn;
    event Action OnAgentRemoved;
    event Action OnAllAgentsCleared;

    void RegisterSpawnedAgent();
    void RemoveRandomAgent();
    void ClearAllAgents();
}
