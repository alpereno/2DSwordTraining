using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSlot : MonoBehaviour
{


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .75f);
    }
}
