// Author:          Scott Krabbenhoft
// Created on:      03/17/2022

// Last edited by: John Mai
// Last edited on:  03/18/2022
// Last edits made: Moved speed value to Enemy class

// Description:     Provides enemy behavior for meat patty enemy type

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeatEnemyBehavior : Enemy
{
    [Header("Meat Enemy Settings")]
    [SerializeField] float maxSpeed = 3f;
    [SerializeField] float deceleration = 3f;
    public int direction = -1;

    BoxCollider2D box = null;
    float radius;
    bool grounded = false;
    

    void Start()
    {
        // get important initial values
        box = GetComponent<BoxCollider2D>();
        radius = box.size.y / 2;
    }

    void Update()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, radius + 0.1f))
        {
            // sets grounded to true if the enemy is on a valid surface
            grounded = true;
        }
        else
        {
            // sets grounded to false if the enemy is not on a valid surface
            grounded = false;
        }
        
        // enemy is set up to move like a slug in short bursts of movement
        // if the enemy has reached the end of a burst of movement, reset speed to max to start a new one
        if ((direction == 1  && speed * direction <= 0) ||
            (direction == -1 && speed * direction >= 0))
        {
            speed = maxSpeed;
        }

        // if the enemy is on the ground, it moves itself and slows down
        if (grounded)
        {
            transform.position += new Vector3 (speed * Time.deltaTime * direction, 0, 0);
            speed -= deceleration * Time.deltaTime;
        }

        // if there is a wall in front of the enemy, turn around
        RaycastHit2D hitCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector3(direction, 0, 0), (box.size.x / 2) + 0.1f);
        RaycastHit2D groundCheck = Physics2D.CircleCast(new Vector2(transform.position.x + direction, transform.position.y - 1), 0.1f, Vector3.up);
        if (hitCheck && hitCheck.collider.gameObject.GetComponent<Tilemap>() != null)
        {
            speed = 0;
            direction *= -1;
        }
        // else if there is a hole in the ground in front of the enemy, turn around
        else if (!groundCheck || (groundCheck && groundCheck.collider.gameObject.GetComponent<Tilemap>() == null))
        {
            speed = 0;
            direction *= -1;
        }
    }

    public void StopEnemy()
    {
        speed = 0;
    }
}
