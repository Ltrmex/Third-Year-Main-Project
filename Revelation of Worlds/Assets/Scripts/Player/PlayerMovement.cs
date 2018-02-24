using System;
using UnityEngine;

/* 
Author: Maciej Majchrzak
Student Number: G00332746
Date: 06-02-2018
Tutorial Reference: https://unity3d.com/learn/tutorials/s/survival-shooter-tutorial
NOTE: Code was developed with the aid of the tutorial mentioned above     
*/

public class PlayerMovement : MonoBehaviour {
    // Public Variables
    public float movementSpeed = 5f;    // movement speed for the player

    // Private Variables
    Vector3 movement;   // direction of the player's movement
    Rigidbody playerRigidbody;  // player's rigidbody.
    Animator myAnimator;
    int floorMask;  // casting of ray at game objects
    float camRayLength = 100f;  // length of the ray from the camera into the scene

    // Called automatically
    public void Awake() {
        // Create a layer mask for the floor layer
        floorMask = LayerMask.GetMask("Floor");

        // Set up references
        myAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }   // Awake

    // Updates player's position
    public void FixedUpdate() {
        // Store the input axes
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Boolean animate = false;

        Move(h, v); // player's movement
        Turning();  // player facing mouse cursor

        // Trying to get the player to jump
        if (Input.GetKeyDown(KeyCode.Space))
            animate = true;

        myAnimator.SetBool("Jump", animate);

    } // FixedUpdate

    // Movement of the player
    public void Move(float h, float v) {
        movement.Set(h, 0f, v); // movement vector based on the axis input
        movement = movement.normalized * movementSpeed * Time.deltaTime;    // make sure movement speed is proportional

        // Move the player
        playerRigidbody.MovePosition(transform.position + movement);

    } // Move

    // Turning of the character towards the direction of the mouse
    public void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); // ray from mouse location in the direction of the camera
        RaycastHit floorHit;    // what's hit by the ray

        // Check if raycast is going to hit the floor
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit
            Vector3 playerToMouse = floorHit.point - transform.position;
            
            playerToMouse.y = 0f;   // ensure the vector is entirely along the floor plane
            
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);    // create a rotation based on the mouse

            // Player's new rotation
            playerRigidbody.MoveRotation(newRotation);
        }   // if
    } // Turning

} // PlayerMovement

