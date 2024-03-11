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
    private HealthManager healthManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        healthManager = GetComponent<HealthManager>();
        agent.speed = speed;
    }

    private void OnEnable()
    {
        healthManager.OnDie += OnDie;
    }

    private void OnDisable()
    {
        healthManager.OnDie -= OnDie;
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

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
