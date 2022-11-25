using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public bool hasSword { get; private set; }

    [SerializeField] private float msBetweenAttacks = 950;    // miliseconds between each attacks (not combo)
    [SerializeField] private bool swordLeftHanded;            // to if sword is on left hand play left handed attack animation

    Animator animator;
    Sword sword;                                              // to apply damage to hittable object
    
    bool appliedFirstAttack; 
    float streakTime = .5f;                                   // combo availability time (sec)
    float nextAttackTime;
    float lastAttackTime;


    private void Start()
    {
        animator = GetComponent<Animator>();
        FindObjectOfType<SnapSlot>().OnSwordSnapped += OnSwordSnapped;        
    }

    public void Attack()
    {
        if (hasSword)
        {
            if (!swordLeftHanded)
            {
                if (appliedFirstAttack && Time.time < lastAttackTime + streakTime)
                {
                    appliedFirstAttack = false;
                    AnimateAttack("rightComboAttack");
                }
                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + msBetweenAttacks / 1000;
                    AnimateAttack("rightAttack");
                    appliedFirstAttack = true;
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + msBetweenAttacks / 1000;
                    AnimateAttack("leftAttack");
                }
            }
        }
    }

    void AnimateAttack(string attackTriggerString)
    {
        animator.SetTrigger(attackTriggerString);
        //sword.ApplyDamage();                              // I didnt call damage method here.
                                                            // Because should not do damage when beginning of the attack
                                                            // It should will call when the sword is pierce
                                                            // so I called it during animation
    }

    // Calling from Unity's animation event
    // being called at different points for each animation (One time, when sword pierced the target object)
    void Pierce() 
    {
        sword.ApplyDamage();
    }

    void OnSwordSnapped(GameObject swordObject)
    {
        sword = swordObject.GetComponent<Sword>();
        hasSword = true;
    }
}
