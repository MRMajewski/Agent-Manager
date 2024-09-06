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

    [Header("Core services references")]
    [SerializeField]
    private AgentService agentService;
    [SerializeField]
    private TickService tickService;

    private void Awake()
    {
        agentService.OnRequestAgentSpawn += Spawn;
        agentService.OnAgentRemoved += RemoveRandomAgent;
        agentService.OnAllAgentsCleared += ClearAllAgents;
        tickService.OnTickRateChanged += ChangeGameSpeed;
    }

    public void Spawn()
    {
        GameObject newAgentGameobject = Instantiate(agentPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        Agent newAgent = newAgentGameobject.GetComponent<Agent>();

        newAgent.Initialize(Guid.NewGuid().ToString());
        newAgent.GetComponent<AgentAIPath>().OnTargetReached += agentService.HandleAgentReachedDestination;
        newAgent.AgentGameObject = newAgentGameobject;

        RegisterSpawnedAgent(newAgent);
        newAgent.SetNewRandomDestination();
    }

    public void RegisterSpawnedAgent(Agent agent)
    {
        agentService.Agents.Add(agent);
    }

    public void RemoveRandomAgent()
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

    public void ChangeGameSpeed(float newSpeed)
    {
        Time.timeScale = newSpeed;
        DOTween.timeScale = Time.timeScale;
    }
    private void OnDestroy()
    {
        agentService.OnRequestAgentSpawn -= Spawn;
        agentService.OnAgentRemoved -= RemoveRandomAgent;
        agentService.OnAllAgentsCleared -= ClearAllAgents;
        tickService.OnTickRateChanged -= ChangeGameSpeed;
    }

    public void Quit()
    {
        Application.Quit();
    }
}