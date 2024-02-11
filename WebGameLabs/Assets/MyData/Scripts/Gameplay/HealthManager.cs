using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamagable
{
    private float health;

    public float Health
    {
        get => health; private set
        {
            health = value;
            OnHealthChange?.Invoke(value);
        }
    }
    [field: SerializeField] public float MaxHealth { get; private set; }

    public event Action<float> OnHealthChange;
    public event Action OnDie;

    private void Start()
    {
        Health = MaxHealth;    
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        Health -= damage;

        if (Health <= 0)
            OnDie?.Invoke();
    }
}
