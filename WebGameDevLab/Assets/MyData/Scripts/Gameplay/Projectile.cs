using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float maxLifetime = 3f;

    private float lifetime = 0f;

    private void OnEnable()
    {
        lifetime = 0;
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > maxLifetime)
        {
            ProjectileManager.Instance.ReturnToPool(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthManager healthManager))
        {
            Debug.Log(other.name);
            healthManager.TakeDamage(damage);
        }
        ProjectileManager.Instance.ReturnToPool(this);
    }
}
