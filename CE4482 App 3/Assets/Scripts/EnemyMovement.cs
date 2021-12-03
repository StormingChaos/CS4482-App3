using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] points;          // array of patrol points
    public float walkSpeed = 0.5f;      // how fast the enemy will patrol between points
    public float chaseSpeed = 2.5f;     // how fast the agent will chase the player
    public float turnSpeed = 1000;      // how fast the agent can turn
    private int destPoint = 0;          // currently selected destination point
    private NavMeshAgent agent;         // reference to Nav mesh agent
    private Transform player;           // reference to player position
    private bool chase;                 // if this enemy is chasing the player or not
    private Animator anim;              // reference to animator
    private EnemyHealth enemyHealth;    // reference to enemyHealth Script
    private PlayerHealth playerHealth;  // reference to player health script

    // Start is called before the first frame update
    private void Start()
    {
        //set up references
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
        // for continuous movement between points
        agent.autoBraking = false;
        chase = false;
        agent.speed = walkSpeed;
        agent.angularSpeed = turnSpeed;

        // begin patrolling
        GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        // returns if no points have been set up
        if (points.Length == 0)
            return;

        // set agent to go to currently selected point
        agent.destination = points[destPoint].position;

        //set agent to animate
        anim.SetBool("IsWalking", true);

        // set next point in array as destination
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0 && !GameStateManager.victory)
        {
            // if this enemy has seen the player, chase them
            if (chase)
            {
                agent.SetDestination(player.position);
            }
            else if (GetComponent<FieldOfView>().canSeePlayer)
            {
                chase = true;
                agent.speed = chaseSpeed;
                anim.SetBool("IsRunning", chase);
            }
            // else choose next destination point when agent gets close to the current one
            else if (!chase && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            chase = false;
            agent.enabled = false;
        }
    }
}
