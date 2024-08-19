using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentService : IAgentService
{
    public event Action<Agent> OnAgentAdded;
    public event Action<Agent> OnAgentRemoved;
    public event Action OnAllAgentsCleared;

    private List<Agent> agents = new List<Agent>();

    [SerializeField]
    private AgentManager agentManager;


    public void AddAgent(Agent agent)
    {
        agents.Add(agent);
        OnAgentAdded?.Invoke(agent); 
    }

    public void ClearAllAgents()
    {
        agents.Clear();
        OnAllAgentsCleared?.Invoke(); // Wysy�amy zdarzenie, �e wszyscy agenci zostali usuni�ci
    }

    public List<Agent> GetAgents()
    {
        return agents;
    }

    public void RemoveRandomAgent()
    {
        if (agents.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, agents.Count);
            Agent agentToRemove = agents[index];
            agents.RemoveAt(index);
            OnAgentRemoved?.Invoke(agentToRemove); // Wysy�amy zdarzenie, �e agent zosta� usuni�ty
        }
    }

    public void RequestAgentSpawn()
    {
        Agent newAgent = agentManager.Spawn();
        AddAgent(newAgent);
    }
}
