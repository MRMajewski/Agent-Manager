using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Agent : MonoBehaviour, IAgent
{
    public event Action<string> OnTargetReached;
    public string GUID { get; private set; }
    [SerializeField]
    private GameObject agentGameObject;
    public GameObject AgentGameObject { get; set; }

    //[SerializeField]
    //private Vector2 moveDurationRange;
    [SerializeField]
    private Vector2 AgentSpawnRange;

    private Vector3 targetPosition;
    private float moveDuration = 4f;
    private float rotationDuration = 0.2f;

    [SerializeField]
    private AIDestinationSetter destinationSetter;

    [SerializeField]
    float checkRadius = 2f;

    bool validPositionFound = false;

    public void Initialize(string guid)
    {
        GUID = guid; 
        destinationSetter.agentsCurrentTarget = targetPosition;
    }
    public void SetNewRandomDestination()
    {
        while (!validPositionFound)
        {
            targetPosition = new Vector3(UnityEngine.Random.Range(
            -1 * AgentSpawnRange.x, AgentSpawnRange.x), 0, UnityEngine.Random.Range(-1 * AgentSpawnRange.y, AgentSpawnRange.y)
            );

            Collider[] hitColliders = Physics.OverlapSphere(targetPosition, checkRadius, LayerMask.GetMask("Walls"));

            if (hitColliders.Length == 0)
            {
                validPositionFound = true;
            }
        }
        validPositionFound = false;
        destinationSetter.agentsCurrentTarget = targetPosition;
    }
          
    //private void SetMovingToDestination()
    //{
    //    moveDuration = UnityEngine.Random.Range(moveDurationRange.x, moveDurationRange.y);
    //    rotationDuration = moveDuration * 0.1f;
    //    RotateTowards();
    //    MoveToTarget();
    //}

    //void RotateTowards()
    //{
    //    Vector3 direction = (targetPosition - transform.position).normalized;
    //    Quaternion targetRotation = Quaternion.LookRotation(direction);

    //    transform.DORotateQuaternion(targetRotation, rotationDuration)
    //        .SetEase(Ease.InOutQuad);
    //}
    private void MoveToTarget()
    {
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
