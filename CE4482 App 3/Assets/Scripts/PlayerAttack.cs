using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;              // damage inflicted by this weapon
    public float attackDelay;       // delay before another attack can be made
    public bool melee;              // is this weapon ranged or melee?
    public int maxAmmo = 1;         // maximum ammo of weapon
    public int ammoCount = 1;       // current ammo of weapon

    float timer = 0;                // timer for cooldown
    Ray bulletTrajectory;           // ray from the gun forwards
    RaycastHit bulletHit;           // raycast hit to get information about what was hit
    LayerMask obstructionMask;      // Layer mask for things that can be shot
    LineRenderer bulletLine;        // Reference to the line renderer
    AudioSource hitSound;           // reference to audio source
    Light bulletFlash;              // reference to light source
    float effectDisplayTime = 0.2f; // how long to show effects for

    private void Awake()
    {
        // create layermask for obstruction layer
        obstructionMask = LayerMask.GetMask("Shootable");
        // set up references
        hitSound = GetComponent<AudioSource>();
        if (!melee)
        {
            bulletLine = GetComponent<LineRenderer>();
            bulletFlash = GetComponent<Light>();
            ammoCount = maxAmmo;
        }
    }

    private void Update()
    {
        // add time since last update call to timer
        timer += Time.deltaTime;

        // if Fire1 is pressed, try to attack
        if(Input.GetButton("Fire1") && timer >= attackDelay)
        {
            // attack if cooldown is over and attack button was pressed
            Attack();
        }

        // if timer has exceeded effect display time, disable the effects
        if (!melee && timer >= attackDelay * effectDisplayTime)
        {
            bulletLine.enabled = false;
            bulletFlash.enabled = false;
        }
    }

    public void Attack()
    {
        // reset timer
        timer = 0f;
        // play attack sudio
        hitSound.Play();

        //if the weapon is melee
        if (melee)
        {

        }
        // if the weapon is not melee, check if it has ammo
        else if (ammoCount > 0)
        {
            // enable light
            bulletFlash.enabled = true;
            // enable line renderer and set first position to gun barrel
            bulletLine.enabled = true;
            bulletLine.SetPosition(0, transform.position);
        }
        // if weapon is not melee but has no ammo, can't attack
    }
}
