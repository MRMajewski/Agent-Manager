using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickService : MonoBehaviour, ITickService
{
    public event Action<float> OnTickRateChanged;

    [SerializeField]
    private float currentTickRate = 1f;
    [SerializeField]
    private float minSpeed = 0f;
    [SerializeField]
    private float maxSpeed = 3f;

    public float CurrentTickRate { get=>currentTickRate; }

    public void SetTickRate(float newTickRate)
    {
        currentTickRate = newTickRate;
        OnTickRateChanged?.Invoke(currentTickRate);
    }
    public void IncreaseTickRate()
    {
        currentTickRate = currentTickRate + .25f;
        currentTickRate = Mathf.Clamp(currentTickRate, minSpeed, maxSpeed);
        OnTickRateChanged?.Invoke(currentTickRate);
    }
    public void DecreaseTickRate()
    {
        currentTickRate = currentTickRate - .25f;
        currentTickRate = Mathf.Clamp(currentTickRate, minSpeed, maxSpeed);
        OnTickRateChanged?.Invoke(currentTickRate);
    }
    public void PauseTime()
    {
        SetTickRate(0);
    }
}
