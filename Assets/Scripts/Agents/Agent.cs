using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public string GUID { get; private set; }  // Unikalny identyfikator agenta

    private Vector3 targetPosition;           // Pozycja, do której agent ma siê przemieœciæ
    private float moveDuration = 5f; 
    private float rotationDuration = 0.2f;          // Czas trwania ruchu agenta

    public event Action<Agent> OnTargetReached;  // Event informuj¹cy, ¿e agent dotar³ do celu

    [SerializeField]
    private Vector2 AgentSpawnRange;

    public void Initialize(string guid)
    {
        // Przypisanie GUID przekazanego przez AgentManager
        GUID = guid;

        // Mo¿na dodaæ inne ustawienia pocz¹tkowe, np. prêdkoœæ, kolory itp.
        Debug.Log($"Agent {GUID} zosta³ zainicjalizowany");

        // Po zainicjalizowaniu, ustaw nowy losowy cel
        SetNewRandomTarget();
    }


    private void SetNewRandomTarget()
    {
        // Wybierz losowy punkt na scenie (np. w granicach 10x10 jednostek od œrodka sceny)
        targetPosition = new Vector3(UnityEngine.Random.Range(-1*AgentSpawnRange.x, AgentSpawnRange.x), 0, UnityEngine.Random.Range(-1 * AgentSpawnRange.y, AgentSpawnRange.y));
        
        RotateTowards();
        MoveToTarget();
    }
    void RotateTowards()
    {
        // Oblicz kierunek do celu
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Oblicz k¹t rotacji w kierunku celu
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Obrót z u¿yciem DOTween
        transform.DORotateQuaternion(targetRotation, rotationDuration)
            .SetEase(Ease.InOutQuad); // Mo¿esz zmieniæ Ease na inny, jeœli chcesz
    }
    private void MoveToTarget()
    {
        // U¿ycie DOTween do poruszania agenta
        transform.DOMove(targetPosition, moveDuration).OnComplete(() =>
        {
            // Gdy agent dotrze do celu, wywo³aj event i ustaw nowy cel
            OnTargetReached?.Invoke(this);
            SetNewRandomTarget();  // Przemieœæ siê do nowego, losowego punktu
        });
    }


}
