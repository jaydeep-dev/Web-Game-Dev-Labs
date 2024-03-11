using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField] private float speed;
    [SerializeField] private float damageInterval;
    [SerializeField] private bool canDamage = false;

    private float currentDamagetime;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    private void FixedUpdate()
    {
        CheckAndDamagePlayer();

        if (agent.pathPending || agent.destination == targetTrans.position)
            return;

        agent.SetDestination(targetTrans.position);

    }

    private void CheckAndDamagePlayer()
    {
        currentDamagetime += Time.fixedDeltaTime;

        if (currentDamagetime > damageInterval)
        {
            canDamage = true;
        }

        if (Vector3.Distance(transform.position, targetTrans.position) <= 1.5f && canDamage)
        {
            targetTrans.GetComponent<IDamagable>().TakeDamage(1);
            canDamage = false;
            currentDamagetime = 0;
        }
    }
}
