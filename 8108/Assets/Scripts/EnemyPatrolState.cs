
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    public float speed;

    
    private NavMeshAgent navMeshAgent;
    FieldofView fov;
    public int randomSpot;

    public int startTime = 3;
    public float countdown;

    public override void EnterState(EnemyControlSystem enemy)
    {
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        fov = enemy.GetComponent<FieldofView>();
        randomSpot = Random.Range(0, enemy.movePoints.Length);
        countdown = startTime;
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        if(fov.canSeePlayer)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        navMeshAgent.destination = enemy.movePoints[randomSpot].position;
        Pathfinding(enemy);
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }

    void Pathfinding(EnemyControlSystem enemy)
    {
        Debug.Log(navMeshAgent.remainingDistance.ToString());
        if (navMeshAgent.remainingDistance <= 0) 
        {
     
            if (countdown == 0)
            {
                randomSpot = Random.Range(0, enemy.movePoints.Length);
                countdown = startTime;
            }
           
                Debug.Log(countdown.ToString());
                countdown-= Time.deltaTime;
                
         
        }
    }

}
