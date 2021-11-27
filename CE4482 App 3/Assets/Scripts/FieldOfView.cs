using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;                  // radius of view
    [Range(0, 360)]                       // limit angle to 0-360 degrees
    public float angle;                   // angle of view

    public GameObject playerRef;          // reference to player

    public LayerMask targetMask;          // mask to target (player)
    public LayerMask obstructionMask;     // mask to obstructions (walls enemy can't see through)

    public bool canSeePlayer;             // boolean determining if the player is in view of the enemy

    private void Start()
    {
        // set up reference to player
        playerRef = GameObject.FindGameObjectWithTag("Player");
        // start FOV coroutine to look for the player
        StartCoroutine(FOVRoutine());
    }

    // coroutine to search for the player every .2 seconds
    private IEnumerator FOVRoutine()
    {
        // how often to search
        float delay = 0.2f;
        // wait for the delay before searching
        WaitForSeconds wait = new WaitForSeconds(delay);

        // dont stop coroutine until object is destroyed
        while(true)
        {
            // wait for delay
            yield return wait;
            // check for the player
            FieldOfViewCheck();
        }
    }

    // function to look for the player
    private void FieldOfViewCheck()
    {
        // collider array to store objects found on the targetMask layer
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // if the array is not empty, that means a collider on the layer was found
        if (rangeChecks.Length != 0)
        {
            // only the player is on the target layerMask, so the player is the first item in the array
            Transform target = rangeChecks[0].transform;
            // set a vector to store the direction to target
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // if the player is within the angle we set, then the player is within the enemy's view
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                //check if the distance to the target is within the enemy's view distance
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // check if there is an object obstructing the view of the player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                //if the raycast hits an object on the obstruction layer mask, then the enemy cannot see the player
                else
                    canSeePlayer = false;
            }
            // if the view angle is greater than the set angle, the player is not within enemy eyesight
            else
                canSeePlayer = false;
        }
        // if the check fails but the boolean was set to true, set it to false since the enemy cannot see the player
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
