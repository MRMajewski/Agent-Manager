using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButtons : MonoBehaviour
{
    [SerializeField]
    private TickService tickService;

     public void IncreaseSpeed()
    {
        tickService.IncreaseTickRate();
    }
    public void DecreaseSpeed()
    {
        tickService.DecreaseTickRate();
    }
    public void PauseGame()
    {
        tickService.PauseTime();
    }
}
