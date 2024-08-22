using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, IAgent
{
    public event Action<string> OnTargetReached;
    public string GUID { get; private set; }
    [SerializeField]
    private GameObject agentGameObject;
    public GameObject AgentGameObject { get; set; }

    [SerializeField]
    private Vector2 moveDurationRange;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private Vector2 AgentSpawnRange;

    private float moveDuration = 5f;
    private float rotationDuration = 0.2f;

    public void Initialize(string guid)
    {
        GUID = guid;
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
            OnTargetReached?.Invoke(this.GUID);
            SetNewRandomDestination();           
        });
    }

    private void OnDestroy()
    {
        this.DOKill(false);
    }
}
