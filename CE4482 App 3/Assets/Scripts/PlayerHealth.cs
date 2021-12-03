using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 1;      // amount of health the player starts with
    public int currentHealth = 1;       // amount of health player currently has
    bool isDead;                        // whether or not player is dead

    Animator anim;                      // reference to animator
    PlayerMovement playerMovement;      // reference to player movement script
    PlayerAttack playerAttack;          // reference to player attacking script

    private void Awake()
    {
        // set up references
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        // set initial player health
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        // reduce player's health by damage amount
        currentHealth -= amount;
        // check if player has died
        if (currentHealth <= 0 && !isDead)
        {
            playerDeath();
        }
    }

    private void playerDeath()
    {
        // set death flag to true
        isDead = true;
        // play death animation
        anim.SetTrigger("PlayerDead");
        // disable movement and attacking
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        // Trigger Game Over
        GameStateManager.gameOver = true;
    }
}
