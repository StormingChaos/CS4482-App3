using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackDelay = 1.5f;     // delay between attacks
    public int attackDamage = 1000;      // how much damage this enemy does
    Animator anim;                       // Reference to the animator component.
    GameObject player;                   // Reference to the player GameObject.
    EnemyHealth enemyHealth;             // Reference to this enemy's health.
    PlayerHealth playerHealth;           // Reference to player health
    bool playerInRange;                  // Whether player is within the trigger collider and can be attacked.
    FieldOfView fov;                     // reference to field of view script
    float timer = 0;                     // timer for attack delay

    private void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player and they are in view
        if (other.gameObject == player && fov.canSeePlayer)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }

    private void Update()
    {
        // add time since last update to timer
        timer += Time.deltaTime;

        // If the player is in range, this enemy is alive, and the attack cooldown has ended
        if (timer >= attackDelay && playerInRange && enemyHealth.currentHealth > 0)
        {
            // check if the player is alive, then attack
            if (playerHealth.currentHealth > 0)
            {
                // reset cooldown timer
                timer = 0;  
                // play attack animation
                anim.SetTrigger("Attack");
                // do damage to the player
                playerHealth.TakeDamage(attackDamage);
            }

        }
    }
}
