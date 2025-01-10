using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHp = 100f;
    public float Hp  { get; protected set; }

    public bool IsDead { get; private set; }

    public event Action onDeath;
    public event Action onDamage;
    protected virtual void OnEnable()
    {
        IsDead = false;
        Hp = maxHp;
    }
    
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        onDamage?.Invoke();
        if (IsDead)
            return;
        Hp -= damage;
        if (Hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath?.Invoke();
        IsDead = true;
        Hp = 0;
    }
}
