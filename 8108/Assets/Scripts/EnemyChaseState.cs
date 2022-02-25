using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private NavMeshAgent navMeshAgent;
   GameObject player;
    
    public override void EnterState(EnemyControlSystem enemy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        navMeshAgent.destination = player.transform.position;
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }
}
