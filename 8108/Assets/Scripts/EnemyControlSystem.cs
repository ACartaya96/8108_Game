using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlSystem : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    FieldofView fov;
    public Transform[] movePoints;
    public Transform playerLastPos;

    EnemyBaseState _state;
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemySearchState SearchState = new EnemySearchState();

    private void Start()
    {
        _state = PatrolState;
        _state.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldofView>();
    }

    private void Update()
    {
        _state.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _state = state;
        state.EnterState(this);
    }

}


