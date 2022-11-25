using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (PlayerCombatController))]
public class Player : MonoBehaviour
{
    // This class takes input and regulates behavior (flipping character, attack etc.)
    // therefore it needs access to other classes (playerController, playerCombatController)

    [SerializeField] private float speed = 3.5f;
    Animator animator;
    PlayerController playerController;
    PlayerCombatController playerCombatController;

    bool facingRight = true;
    bool attacking;             // when player attacking disable moving
    float velocity;
    float attackTime = 1f;      // disable time

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerCombatController = GetComponent<PlayerCombatController>();
    }

    private void Update()
    {
        MoveInput();
        AttackInput();        
    }

    void MoveInput()
    {
        if (!attacking)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            velocity = inputX * speed;

            if (velocity > 0 && !facingRight)
            {
                Flip();
            }
            else if (velocity < 0 && facingRight)
            {
                Flip();
            }

            playerController.SetVelocity(velocity);
            Animating(velocity);
        }
    }

    void AttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerCombatController.hasSword)
            {
                attacking = true;
                velocity = 0;
                playerController.SetVelocity(velocity);
                Animating(velocity);
                playerCombatController.Attack();

                Invoke("EndTheAttack", attackTime);
            }
        }
    }

    void Animating(float value)
    {
        bool running = value != 0;
        animator.SetBool("running", running);
    }

    // for flipping character when input values opposite of the direction 
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void EndTheAttack()
    {
        attacking = false;
    }
}
