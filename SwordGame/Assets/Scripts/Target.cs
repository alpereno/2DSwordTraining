using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private ParticleSystem damageEffect;
    [SerializeField] private ParticleSystem deathEffect;
    bool dead;

    public void TakeDamage(float damage, Vector3 hitPoint)
    {        
        health -= damage;
        if (health <= 0 && !dead)
        {
            if (damageEffect != null)
            {
                deathEffect.Play();
            }
            Die();
            return;
        }
        if (damageEffect != null)
        {
            damageEffect.Play();
        }        
    }

    void Die()
    {
        health = 0;
        dead = true;
        GameObject.Destroy(gameObject);
    }
}
