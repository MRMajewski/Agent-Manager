using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentService : MonoBehaviour, IAgentService
{
    public event Action<Agent> OnAgentAdded;
    public event Action<Agent> OnAgentRemoved;
    public event Action OnAllAgentsCleared;

    private List<Agent> agents = new List<Agent>();

    public List<Agent> Agents { get; set; }

    [SerializeField]
    private AgentManager agentManager;

    public void RequestAgentSpawn()
    {
        Agent newAgent = agentManager.Spawn();
        AddAgent(newAgent);
    }
    public void AddAgent(Agent agent)
    {
        agents.Add(agent);
        OnAgentAdded?.Invoke(agent); 
    }

    public void RemoveRandomAgent()
    {
        if (agents.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, agents.Count);
            Agent agentToRemove = agents[index];
            agents.RemoveAt(index);
            OnAgentRemoved?.Invoke(agentToRemove);
            agentManager.RemoveAgent(agentToRemove);
        }
    }

    public void ClearAllAgents()
    {
        agentManager.ClearAllAgents();
        agents.Clear();
        OnAllAgentsCleared?.Invoke();
    }

}
