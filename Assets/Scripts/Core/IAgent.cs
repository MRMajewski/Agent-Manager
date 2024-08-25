using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IAgent
{
    public string GUID { get; }
    public void SetNewRandomDestination();
    public GameObject AgentGameObject { get; set; }
    public event Action<string> OnTargetReached;
}
