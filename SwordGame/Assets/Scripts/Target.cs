using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        print("damage taken = " + damage + "pos = " + hitPoint);
    }
}
