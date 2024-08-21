using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, IAgent
{
    public string GUID { get; private set; }
    [SerializeField]
    private GameObject agentGameObject;
    public GameObject AgentGameObject { get; set; }

    [SerializeField]
    private Vector2 moveDurationRange;
    [SerializeField]


    private Vector3 targetPosition;
    private float moveDuration = 5f;
    private float rotationDuration = 0.2f;

    public event Action<Agent> OnTargetReached;

    [SerializeField]
    private Vector2 AgentSpawnRange;

    public void Initialize(string guid)
    {
        GUID = guid;

        Debug.Log($"Agent {GUID} zosta³ zainicjalizowany");

        SetNewRandomDestination();
    }

    public void SetNewRandomDestination()
    {
        targetPosition = new Vector3(UnityEngine.Random.Range(
            -1 * AgentSpawnRange.x, AgentSpawnRange.x), 0, UnityEngine.Random.Range(-1 * AgentSpawnRange.y, AgentSpawnRange.y)
            );

        RotateTowards();
        MoveToTarget();
    }
    void RotateTowards()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.DORotateQuaternion(targetRotation, rotationDuration)
            .SetEase(Ease.InOutQuad);
    }
    private void MoveToTarget()
    {
        moveDuration = UnityEngine.Random.Range(moveDurationRange.x, moveDurationRange.y);

        transform.DOMove(targetPosition, moveDuration).OnComplete(() =>
        {
            OnTargetReached?.Invoke(this);
            SetNewRandomDestination();
        });
    }

    private void OnDestroy()
    {

    }
}
