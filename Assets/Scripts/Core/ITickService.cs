using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITickService
{
    event Action<float> OnTickRateChanged;
    void SetTickRate(float newTickRate);
}
