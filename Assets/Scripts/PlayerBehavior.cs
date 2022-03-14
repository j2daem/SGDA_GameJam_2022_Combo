// Author:          Scott Krabbenhoft
// Created on:      03/13/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/13/2022

// Description:     Handles player behaviors, which is just movement for now

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpSpeed = 4f;

    Rigidbody2D rigidBody;
    CapsuleCollider2D capsule;
    float radius;
    bool grounded = false;
    bool doubleJump = false;
    
    void Start()
    {
        // gets some values to make later manipulation easier
        rigidBody = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        radius = capsule.size.y / 2;
    }

    void Update()
    {
        // checks for any collider directly under the player -- NOTE: this will include any enemies or objects that have colliders,
        //                                                            so collision masks may be necessary in the future
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, radius + 0.1f))
            {
                // sets grounded and double jump to true if the player is on a valid surface
                grounded = true;
                doubleJump = true;
            }
            else
            {
                // sets grounded to false if the player is not on a valid surface -- NOTE: double jump should not be edited here
                grounded = false;
            }
        // forces the player upwards when the jump button is hit, accounting for gravity
        if (Input.GetButtonDown("Jump") && (grounded || doubleJump))
        {
            rigidBody.AddForce(new Vector2(0, jumpSpeed - rigidBody.velocity.y), ForceMode2D.Impulse);
            // sets double jump to false if player jumped while airborne
            if (!grounded && doubleJump)
            {
                doubleJump = false;
            }
        }

        // manually adds x movement to position
        rigidBody.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        // zeroes out x velocity to prevent sliding errors
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
        
    }
}
