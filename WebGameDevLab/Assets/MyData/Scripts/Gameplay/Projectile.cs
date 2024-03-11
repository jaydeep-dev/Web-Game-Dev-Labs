using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthManager healthManager))
        {
            Debug.Log(other.name);
            healthManager.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
