using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAgent
{
    public string GUID { get; }
    public void SetNewRandomDestination();
    public GameObject AgentGameObject { get; set; }
}
public class AgentService : MonoBehaviour, IAgentService
{
    public event Action OnRequestAgentSpawn;
    public event Action OnAgentRemoved;
    public event Action OnAllAgentsCleared;

    private List<IAgent> agents = new List<IAgent>();

    [SerializeField]
    public List<IAgent> Agents { get => agents; set => agents = value; }

    public void RequestAgentSpawn()
    {
        OnRequestAgentSpawn?.Invoke();
    }

    public void RemoveRandomAgent()
    {
        OnAgentRemoved.Invoke();
    }

    public void ClearAllAgents()
    {
        OnAllAgentsCleared?.Invoke();
    }

    public void RegisterSpawnedAgent()
    {
    }
}
