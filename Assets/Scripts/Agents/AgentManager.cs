using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    private GameObject agentPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private AgentService agentService;

    public event Action<Agent> OnAgentSpawned;

    private void Awake()
    {
        agentService.OnRequestAgentSpawn += Spawn;
        agentService.OnAgentRemoved += RemoveAgent;
        agentService.OnAllAgentsCleared += ClearAllAgents;
    }

    public void Spawn()
    {
        GameObject newAgentGameobject = Instantiate(agentPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);

        Agent newAgent = newAgentGameobject.GetComponent<Agent>();
        newAgent.AgentGameObject = newAgentGameobject;
        newAgent.Initialize(Guid.NewGuid().ToString());

        OnAgentSpawned?.Invoke(newAgent);
        RegisterSpawnedAgent(newAgent);
    }

    public void RegisterSpawnedAgent(Agent agent)
    {
        agentService.Agents.Add(agent);
    }

    public void ChangeGameSpeed(float newSpeed)
    {
        Time.timeScale = newSpeed;
        DOTween.timeScale = newSpeed;
    }

    public void RemoveAgent()
    {
        if (agentService.Agents.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, agentService.Agents.Count);
            IAgent agentToRemove = agentService.Agents[index];

            Destroy(agentToRemove.AgentGameObject);
            agentService.Agents.RemoveAt(index);
        }
    }

    public void ClearAllAgents()
    {
        foreach (Agent agent in agentService.Agents)
        {
            if (DOTween.IsTweening(agent))
            {
                DOTween.Kill(agent, false);
            }
            Destroy(agent.AgentGameObject);
        }
        agentService.Agents.Clear();
    }
}
