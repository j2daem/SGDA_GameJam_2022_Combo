// Author:          Scott Krabbenhoft
// Created on:      03/13/2022

// Last edited by:  John Mai
// Last edited on:  03/15/2022, 2:34 PM
// Last made edit:  Added flip script to allow player sprite to face left and right accordingly

// Description:     Handles player behaviors, which is just movement for now

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] OrderGenerator order;
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpSpeed = 4f;
    public int lives = 3;

    Rigidbody2D rigidBody = null;
    CapsuleCollider2D capsule = null;
    float radius;
    bool grounded = false;
    bool doubleJump = false;
    bool facingRight = true;

    public int score;
    
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

        // If player moves left while facing right, flip the player to the left
        if ((Input.GetAxis("Horizontal") < 0) && facingRight)
        {
            Flip();
        }
        // If plyer moves right while facing left, flip player to right
        else if ((Input.GetAxis("Horizontal") > 0) && !facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1, flipping them
        transform.Rotate(0f, 180f, 0f);
    }

    public void CollectIngredient(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.lettuce:
                order.orderProgress[0]++;
                break;
            case IngredientType.cheese:
                order.orderProgress[1]++;
                break;
            case IngredientType.onion:
                order.orderProgress[2]++;
                break;
            case IngredientType.tomato:
                order.orderProgress[3]++;
                break;
            case IngredientType.pickle:
                order.orderProgress[4]++;
                break;
            case IngredientType.meat:
                order.orderProgress[5]++;
                break;
        }
    }
}
