using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AgentAmountLabel : MonoBehaviour
{
    [SerializeField]
    private AgentService agentService;

    [SerializeField]
    private TextMeshProUGUI agentAmountLabel;

    private void Awake()
    {
        agentService.OnAgentNumberChanged += UpdateAgentAmountLabel;
    }
    private void Start()
    {
        UpdateAgentAmountLabel();
    }
    public void UpdateAgentAmountLabel()
    {
        agentAmountLabel.text = "Agents: "+ agentService.Agents.Count;
    }

    private void OnDestroy()
    {
        agentService.OnAgentNumberChanged -= UpdateAgentAmountLabel;
    }

}
