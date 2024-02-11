using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovements : MonoBehaviour
{
    [SerializeField] private bool useNavmesh;
    [SerializeField] private Transform targetTrans;
    [SerializeField] private float speed;
    [SerializeField, Range(.1f, 1f)] private float reTargetTime;

    private NavMeshAgent agent;

    private void Awake()
    {
        if (useNavmesh)
            agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (!useNavmesh)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, Time.fixedDeltaTime * speed);
            if (Vector3.Distance(transform.position, targetTrans.position) <= 1.5f)
            {
                targetTrans.GetComponent<IDamagable>().TakeDamage(1);
            }

            transform.LookAt(targetTrans, Vector3.up);
        }
        else
        {
            if (!agent.pathPending && agent.destination != targetTrans.position && agent.remainingDistance < 1f)
            {
                agent.SetDestination(targetTrans.position);
            }
        }
    }
}
