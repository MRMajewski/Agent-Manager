using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAgentService
{
    event Action<Agent> OnAgentAdded;
    event Action<Agent> OnAgentRemoved;
    event Action OnAllAgentsCleared;


    void AddAgent(Agent agent);
    void RemoveRandomAgent();
    void ClearAllAgents();

    public void RequestAgentSpawn();
}
