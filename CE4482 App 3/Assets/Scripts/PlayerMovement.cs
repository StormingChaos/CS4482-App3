using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movementDirection;          // The vector to store the direction of the player's movement.
    public static Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidBody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    Vector3 lookDirection;              // Direction the player is moving

    //runs on program start
    private void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    //runs every physics step
    private void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        if (!GameStateManager.getInput)
        {
            // Move the player around the scene.
            Move(h, v);
            // Turn the player to face the mouse cursor.
            Turning();
            // Animate the player.
            Animating(h, v);
        }
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movementDirection.Set(h, 0f, v);
        // Normalise the movement vector and make it proportional to the speed per second.
        movementDirection = movementDirection.normalized * speed * Time.deltaTime;
        // Move the player to it's current position plus the movement.
        playerRigidBody.MovePosition(transform.position + movementDirection);
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            lookDirection = floorHit.point - transform.position;
            // Ensure the vector is entirely along the floor plane.
            lookDirection.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            // Set the player's rotation to this new rotation.
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool moving = h != 0f || v != 0f;
        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsMoving", moving);

        Vector3 moveDirection = new Vector3(h, 0, v);

        if (moveDirection.magnitude > 1f)
        {
            moveDirection = moveDirection.normalized;
        }
        moveDirection = transform.InverseTransformDirection(moveDirection);

        anim.SetFloat("MovementVertical", moveDirection.z, 0.05f, Time.deltaTime);
        anim.SetFloat("MovementHorizontal", moveDirection.x, 0.05f, Time.deltaTime);


    }
}
