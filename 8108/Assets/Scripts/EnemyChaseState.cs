using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private NavMeshAgent navMeshAgent;
    FieldofView fov;
    GameObject player;
    
    public override void EnterState(EnemyControlSystem enemy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fov = enemy.GetComponent<FieldofView>();
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        if(!fov.canSeePlayer)
        {
            enemy.playerLastPos.position = player.transform.position;
            navMeshAgent.destination = enemy.playerLastPos.position;
            Debug.Log(enemy.name + ": Lost Sight of 8108 sweeping the area");
            enemy.SwitchState(enemy.SearchState);
        }
        navMeshAgent.destination = player.transform.position;
        
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }
}
