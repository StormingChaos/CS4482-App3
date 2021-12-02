using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;             // the maximum health of this enemy
    public int currentHealth;         // amount of health the enemy currently has
    private bool isDead;              // whether or not the enemy is dead

    public GameObject droppedItem;    // item that this enemy will drop when it dies
    public float dropChance;          // chance that this enemy will drop an item (percent 0-100)

    Animator anim;                    // reference to animator component
    CapsuleCollider capsuleCollider;  // Reference to the capsule collider.


    // Start is called before the first frame update
    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = maxHealth;      // set the current health to max health once the game starts
    }

    public void TakeDamage(int amount)
    {
        // if the enemy is dead and somehow took damage, return
        if (isDead)
            return;

        // subtract the damage from the enemy's current health
        currentHealth -= amount;

        // if health is less than 0...
        if (currentHealth <= 0)
        {
            // set enemy to dead
            isDead = true;
            // call death animation
            anim.SetTrigger("IsDead");
            GameStateManager.kills++;
        }
    }

    //enemy will drop an item once it dies
    void DropItem()
    {
        //make sure enemy is actually dead and has a drop
        if (isDead && droppedItem != null)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x, 1, transform.position.y);
            //set up random chance
            if (Random.Range(0, 100) <= dropChance)
            {
                Instantiate(droppedItem, spawnPoint, transform.rotation);
            }
        }
    }
}
