using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public float speed;

    public Transform[] movePoints;
    private NavMeshAgent navMeshAgent;
    public int randomSpot;

    /*public Patrol(AIBehaviorSystem behaviorSystem) : base(behaviorSystem)
    {
        
    }*/

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        randomSpot = Random.Range(0, movePoints.Length);
    }

    void Update()
    {
        if(navMeshAgent.remainingDistance <= 0)
        {
            randomSpot = Random.Range(0, movePoints.Length);
        }
        navMeshAgent.destination = movePoints[randomSpot].position;
    }
}