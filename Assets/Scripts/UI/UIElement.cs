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
    private CanvasGroup messageLabelCanvasGroup;  // Przypisz CanvasGroup w Inspectorze
    [SerializeField]
    private float blinkDuration = 0.35f;
    private void Awake()
    {
        agentService.OnAgentReachedDestination += DisplayArrivalMessage;
    }

    private void DisplayArrivalMessage(string agentGuid)
    {
        Debug.Log("BLINK");
        Blink();
        messageLabel.text = $"Agent {agentGuid} arrived at destination!";
 
    }

    void Blink()
    {
        this.DOKill(false);
        // Tworzymy sekwencjê
        Sequence blinkSequence = DOTween.Sequence();

        // Nastêpnie animacjê powrotu alpha do 1
        blinkSequence.Append(messageLabelCanvasGroup.DOFade(1, blinkDuration));
        blinkSequence.AppendInterval(2f);
        blinkSequence.Append(messageLabelCanvasGroup.DOFade(0, blinkDuration));

    }
}
