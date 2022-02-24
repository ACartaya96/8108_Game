
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    public float speed;

    public Transform[] movePoints;
    private NavMeshAgent navMeshAgent;
    public int randomSpot;

    public int startTime = 3;
    public float countdown;
    bool canSeePlayer;
    public override void EnterState(EnemyControlSystem enemy)
    {
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        randomSpot = Random.Range(0, movePoints.Length);
        countdown = startTime;
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        LookingforPlayer(canSeePlayer);
        Pathfinding();
        if(canSeePlayer)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        navMeshAgent.destination = movePoints[randomSpot].position;
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }

    void Pathfinding()
    {
        if (Vector3.Distance(navMeshAgent.destination, movePoints[randomSpot].position) <= 0)
        {
            if (countdown != 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                randomSpot = Random.Range(0, movePoints.Length);
                countdown = startTime;
            }
        }
    }

    bool LookingforPlayer(bool spotted)
    {
        //RayCast Logic
        return spotted;
    }

}
