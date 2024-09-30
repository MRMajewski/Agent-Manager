using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTimeLabel : MonoBehaviour
{
    [SerializeField]
    private TickService tickService;

    [SerializeField]
    private TextMeshProUGUI currentTickRateLabel;

    private void Awake()
    {
        tickService.OnTickRateChanged += UpdateTickRateLabel;
    }
    private void Start()
    {
        UpdateTickRateLabel(tickService.CurrentTickRate);
    }
    public void UpdateTickRateLabel(float tickRate)
    {
        currentTickRateLabel.text = "Time speed: " + tickRate.ToString("F2");
    }
    private void OnDestroy()
    {
        tickService.OnTickRateChanged -= UpdateTickRateLabel;
    }
}
