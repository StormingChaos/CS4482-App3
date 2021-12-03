using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject pickupWeapon;         // reference to this pickup's equipped weapon
    public GameObject bat;                  // reference to player equipped bat
    public GameObject pistol;               // reference to player equipped pistol
    public GameObject rifle;                // reference to player equipped rifle
    GameObject player;                      // reference to player

    public void OnColliderEnter(Collider other)
    {
        // check if entering collider is the player
        if (other == player)
        {
            // check if player pressed RMB
            //if (GetInput)
            {
                // disable equipped weapons
                bat.SetActive(false);
                pistol.SetActive(false);
                rifle.SetActive(false);
                // enable this weapon
                pickupWeapon.SetActive(true);
                // disable this pickup
                //Destroy();
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
