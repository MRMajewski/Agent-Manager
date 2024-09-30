using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIElement : MonoBehaviour
{
    [SerializeField]
    private AgentService agentService;

    [SerializeField] 
    private TextMeshProUGUI messageLabel;
    [SerializeField]
    private CanvasGroup messageLabelCanvasGroup; 
    [SerializeField]
    private float blinkDuration = 0.35f;
    private void Awake()
    {
        agentService.OnAgentReachedDestination += DisplayArrivalMessage;
    }

    private void DisplayArrivalMessage(string agentGuid)
    {
        Blink();
        messageLabel.text = $"Agent {agentGuid}<br>arrived at destination!";
    }

    void Blink()
    {
        this.DOKill(false);

        Sequence blinkSequence = DOTween.Sequence();
        blinkSequence.Append(messageLabelCanvasGroup.DOFade(1, blinkDuration));
        blinkSequence.AppendInterval(2f);
        blinkSequence.Append(messageLabelCanvasGroup.DOFade(0, blinkDuration));
    }
}
