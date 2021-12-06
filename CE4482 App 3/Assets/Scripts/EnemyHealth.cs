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

    public bool damaged;             // if the enemy has taken damage


    // Start is called before the first frame update
    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = maxHealth;      // set the current health to max health once the game starts
        damaged = false;                // enemy starts having not taken damage
    }

    public void TakeDamage(int amount)
    {
        // if the enemy is dead and somehow took damage, return
        if (isDead)
            return;

        // subtract the damage from the enemy's current health
        currentHealth -= amount;
        // set damaged flag to true
        damaged = true;

        // if health is less than 0...
        if (currentHealth <= 0)
        {
            // set enemy to dead
            isDead = true;
            // call death animation
            anim.SetTrigger("IsDead");
            GameStateManager.kills++;

            // drop item
            DropItem();
        }
    }

    //enemy will drop an item once it dies
    void DropItem()
    {
        //make sure enemy is actually dead and has a drop
        if (isDead && droppedItem != null)
        {
            // set spawn point to where the enemy was
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            // set item rotation
            Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);

            //set up random chance
            if (Random.Range(0, 100) <= dropChance)
            {
                Instantiate(droppedItem, spawnPoint, rotation);
            }
        }
    }
}
