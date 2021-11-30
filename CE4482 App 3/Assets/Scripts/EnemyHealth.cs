using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;           // the maximum health of this enemy
    public int currentHealth;       // amount of health the enemy currently has

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;      // set the current health to max health once the game starts
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
