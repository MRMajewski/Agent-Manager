using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAgent
{
    public string GUID { get; }
    public void SetNewRandomDestination();
    public GameObject AgentGameObject { get; set; }
    public event Action<string> OnTargetReached;
}


public class AgentService : MonoBehaviour, IAgentService
{
    public event Action OnRequestAgentSpawn;
    public event Action OnAgentRemoved;
    public event Action OnAllAgentsCleared;

    public event Action OnAgentNumberChanged;

    public event Action<string> OnAgentReachedDestination;

    private List<IAgent> agents = new List<IAgent>();

    [SerializeField]
    public List<IAgent> Agents { get => agents; set => agents = value; }

    public void RequestAgentSpawn()
    {
        OnRequestAgentSpawn.Invoke();
        OnAgentNumberChanged.Invoke();
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
        Debug.Log($"Agent {agentGuid} dotar³ do celu.");
        OnAgentReachedDestination?.Invoke(agentGuid);
    }
}
