using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform player;
    public override void EnterState(EnemyControlSystem enemy)
    {
       
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        navMeshAgent.destination = player.position;
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }
}
