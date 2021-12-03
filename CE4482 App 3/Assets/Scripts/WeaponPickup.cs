using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    GameObject pickupWeapon;         // reference to this pickup's equipped weapon
    GameObject player;               // reference to player

    private void OnTriggerEnter(Collider other)
    {
        // check if entering collider is the player
        if (other.gameObject == player)
        {
            // check what weapon this is
            // enable this weapon and disable other weapons
            if (this.CompareTag("BatPickup"))
            {
                GameStateManager.pistol.SetActive(false);
                GameStateManager.rifle.SetActive(false);
                GameStateManager.bat.SetActive(true);
            }
            else if (this.CompareTag("PistolPickup"))
            {
                GameStateManager.bat.SetActive(false);
                GameStateManager.rifle.SetActive(false);
                GameStateManager.pistol.SetActive(true);
                GameStateManager.pistol.GetComponentInChildren<PlayerAttack>().ammoCount = GameStateManager.pistol.GetComponentInChildren<PlayerAttack>().maxAmmo;
            }
            else if (this.CompareTag("RiflePickup"))
            {
                GameStateManager.rifle.SetActive(true);
                GameStateManager.bat.SetActive(false);
                GameStateManager.pistol.SetActive(false);
                GameStateManager.rifle.GetComponentInChildren<PlayerAttack>().ammoCount = GameStateManager.rifle.GetComponentInChildren<PlayerAttack>().maxAmmo;
            }
            // disable this pickup
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
