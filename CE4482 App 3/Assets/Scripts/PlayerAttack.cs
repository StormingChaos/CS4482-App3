using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    public int damage;                  // damage inflicted by this weapon
    public float attackDelay;           // delay before another attack can be made
    public bool melee;                  // is this weapon ranged or melee?
    public int maxAmmo = 1;             // maximum ammo of weapon
    public int ammoCount = 1;           // current ammo of weapon
    public float range;                 // the range of the weapon

    float timer = 0;                    // timer for cooldown
    Ray hitRay;                         // ray from the weapon forwards
    RaycastHit weaponHit;               // raycast hit to get information about what was hit
    LineRenderer bulletLine;            // Reference to the line renderer
    AudioSource hitSound;               // reference to audio source
    Light bulletFlash;                  // reference to light source
    float effectDisplayTime = 0.2f;     // how long to show effects for
    GameObject player;                  // reference to player
    Animator anim;                      // reference to player animator

    public TextMeshProUGUI ammoText;    // reference to ammo text ui component

    private void Awake()
    {
        // set up references
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        anim.fireEvents = false;
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
        if(Input.GetButton("Fire1") && timer >= attackDelay && ammoCount > 0)
        {
            // attack if cooldown is over, attack button was pressed and weapon has ammo
            Attack();
        }

        // update ammo UI
        ammoText.text = "Ammo: " + ammoCount + "/" + maxAmmo;

        // if timer has exceeded effect display time, disable the effects
        if (!melee && timer >= attackDelay * effectDisplayTime)
        {
            bulletLine.enabled = false;
            bulletFlash.enabled = false;
            // set color
            bulletLine.startColor = Color.yellow;
            bulletLine.endColor = Color.yellow;
        }
    }

    public void Attack()
    {
        // reset timer
        timer = 0f;

        // play attack animation
        if (melee)
        {
            anim.SetTrigger("AttackMelee");
        }
        else
        {
            // play attack audio
            hitSound.Play();
            // enable light
            bulletFlash.enabled = true;
            // enable line renderer and set first position to gun barrel
            bulletLine.enabled = true;
            bulletLine.SetPosition(0, player.transform.position);
            anim.SetTrigger("AttackGun");
            ammoCount--;
        }
        // set the ray to start at the player and point forwards
        hitRay.origin = player.transform.position;
        hitRay.direction = player.transform.forward;

        // perform raycast against gameobjects on the obstruction layer
        if (Physics.Raycast(hitRay, out weaponHit, range))
        {
            // find an enemyHealth script on the gameobject hit
            EnemyHealth enemyHealth = weaponHit.collider.GetComponent<EnemyHealth>();
            // if the enemyHealth component exists, they should take damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                // play the melee hit sound since it hit an enemy
                if (melee)
                {
                    hitSound.Play();
                }
            }

            // set second position of line renderer to the point the raycast hit
            if (!melee)
            {
                bulletLine.SetPosition(1, weaponHit.point);
            }
        }
        // if the raycast didn't hit anything
        else if(!melee)
        {
            // set second position of line renderer to the max range of weapon
            bulletLine.SetPosition(1, hitRay.origin + hitRay.direction * range);
        }
    }
}
