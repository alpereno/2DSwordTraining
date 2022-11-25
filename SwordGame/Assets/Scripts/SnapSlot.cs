using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSlot : MonoBehaviour
{
    // The pieces of code here are not important for the given tasks.

    // These codes here are for the attack when the sword snapping by any hand

    public event System.Action <GameObject> OnSwordSnapped;
    public void SwordSnap(GameObject swordObject)
    {
        if (OnSwordSnapped != null)
        {
            OnSwordSnapped(swordObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .75f);
    }
}
