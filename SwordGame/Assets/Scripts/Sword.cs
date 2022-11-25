using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform attackPointTransform;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private LayerMask hittableObjectMask;
    [SerializeField] private float damage = 5;


    // Does Damage the target object(s)
    public void ApplyDamage()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPointTransform.position, attackRange, hittableObjectMask);
        foreach (Collider2D hitCollider in hitObjects)
        {
            print("damage has been applied = " + hitCollider.name);
            IDamageable damageableObject = hitCollider.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage, attackPointTransform.position);
            }
        }        
    }

    private void OnDrawGizmos()
    {        
        if (attackPointTransform != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(attackPointTransform.position, attackRange);
        }
    }
}
