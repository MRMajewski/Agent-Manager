using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    private List<Agent> agents = new List<Agent>();

    [SerializeField] private GameObject agentPrefab; 
    [SerializeField] private Transform spawnPoint; 

    [ContextMenu("Spawn")]
    public Agent Spawn()
    {
        GameObject newAgentGameobject = Instantiate(agentPrefab, spawnPoint.position, Quaternion.identity,spawnPoint);

        Agent newAgent = newAgentGameobject.GetComponent<Agent>();

        newAgent.Initialize(Guid.NewGuid().ToString());

        agents.Add(newAgent);

        return newAgent;
    }


    public void RegisterSpawnedAgent(Agent agent)
    {

    }

    public void ChangeGameSpeed(float newSpeed)
    {
        Time.timeScale = newSpeed;
        DOTween.timeScale = newSpeed;
    }

    public void RemoveAgent(Agent agent)
    {
        if (agents.Contains(agent))
        {
            agents.Remove(agent);
            Destroy(agent.gameObject); 
        }
    }

    public void ClearAllAgents()
    {
        foreach (Agent agent in agents)
        {
            Destroy(agent.gameObject); 
        }
        agents.Clear(); 
    }
}
