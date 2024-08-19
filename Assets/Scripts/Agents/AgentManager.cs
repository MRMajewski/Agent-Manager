using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    private Agent agentTemplate;

    public Agent Spawn()
    {
        Agent newAgent = Instantiate(agentTemplate);

        return newAgent;
    }
    public void ChangeSpeed()
    {
    }
}
