using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovements : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField, Range(.1f, 1f)] private float reTargetTime;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SetTargetDestination), 0, reTargetTime);
    }

    private void SetTargetDestination()
    {
        if (agent.destination != targetTrans.position)
            agent.SetDestination(targetTrans.position);
    }
}
