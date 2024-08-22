using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentButtons : MonoBehaviour
{
    [SerializeField]
    private AgentService agentService;

    public void AddAgent()
    {
        agentService.RequestAgentSpawn();
    }
    public void RemoveRandomAgent()
    {
        agentService.RemoveRandomAgent();
    }
    public void ClearAllAgents()
    {
        agentService.ClearAllAgents();
    }
}
