using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    float velocity;    

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + Vector2.right * velocity * Time.fixedDeltaTime);        
    }

    public void SetVelocity(float _velocity)
    {
        velocity = _velocity;
    }
}