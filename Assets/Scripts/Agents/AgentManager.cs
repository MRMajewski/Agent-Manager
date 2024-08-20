using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    private List<Agent> agents = new List<Agent>();

    [SerializeField] private GameObject agentPrefab; // Prefab agenta
    [SerializeField] private Transform spawnPoint; 
    //[SerializeField] private Transform[] destinationPoints;

    [ContextMenu("Spawn")]
    public Agent Spawn()
    {
        GameObject newAgentGameobject = Instantiate(agentPrefab, spawnPoint.position, Quaternion.identity,spawnPoint);

        // Pobranie komponentu Agent
        Agent newAgent = newAgentGameobject.GetComponent<Agent>();

        // Generowanie unikalnego GUID dla agenta
        newAgent.Initialize(Guid.NewGuid().ToString());

        // Losowanie celu agenta i uruchamianie jego ruchu
     //   AssignNewDestination(newAgent);

        // Dodanie agenta do listy
        agents.Add(newAgent);

        return newAgent;
    }

    //public void AssignNewDestination(Agent agent)
    //{
    //    if (destinationPoints.Length == 0) return;

    //    // Losowanie nowego celu z dostêpnych punktów
    //    Transform randomDestination = destinationPoints[UnityEngine.Random.Range(0, destinationPoints.Length)];

    //    // Przemieszczenie agenta do nowego celu za pomoc¹ DOTween
    //    agent.transform.DOMove(randomDestination.position, 5f).OnComplete(() =>
    //    {
    //        // Po dotarciu do celu, przypisz nowy cel
    //        AssignNewDestination(agent);

    //        // Wyœwietl komunikat o dotarciu agenta do celu
    //        Debug.Log($"Agent {agent.GUID} arrived at destination.");
    //    });
    //}
    public void ChangeGameSpeed(float newSpeed)
    {
        Time.timeScale = newSpeed;
        DOTween.timeScale = newSpeed;
    }

    public void RemoveAgent(Agent agent)
    {
        if (agents.Contains(agent))
        {
            agents.Remove(agent);
            UnityEngine.Object.Destroy(agent.gameObject); // Usuniêcie agenta z gry
        }
    }

    // Metoda do czyszczenia wszystkich agentów (opcjonalnie)
    public void ClearAllAgents()
    {
        foreach (Agent agent in agents)
        {
            UnityEngine.Object.Destroy(agent.gameObject); // Usuwanie wszystkich agentów
        }
        agents.Clear(); // Oczyszczanie listy agentów
    }
}
