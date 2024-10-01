using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AgentService : MonoBehaviour, IAgentService
{
    public event Action OnRequestAgentSpawn;
    public event Action OnAgentRemoved;
    public event Action OnAllAgentsCleared;
    public event Action OnAgentNumberChanged;
    public event Action<string> OnAgentReachedDestination;

    private List<IAgent> agents = new List<IAgent>();
    public List<IAgent> Agents { get => agents; set => agents = value; }

    public void RequestAgentSpawn()
    {
        if (Agents.Count < 100)
        {
            OnRequestAgentSpawn.Invoke();
            OnAgentNumberChanged.Invoke();
        }
   
    }
    public void RemoveRandomAgent()
    {
        OnAgentRemoved.Invoke();
        OnAgentNumberChanged.Invoke();
    }
    public void ClearAllAgents()
    {
        OnAllAgentsCleared?.Invoke();
        OnAgentNumberChanged.Invoke();
    }
    public void HandleAgentReachedDestination(string agentGuid)
    {
        OnAgentReachedDestination?.Invoke(agentGuid);
    }
}
