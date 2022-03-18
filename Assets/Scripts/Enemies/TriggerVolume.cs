// Author:          Scott Krabbenhoft
// Created on:      03/17/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/17/2022

// Description:     Provides damage trigger volume for enemies that deal contact damage -- currently only implemented for meat;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    Enemy parentEnemy;
    
    void Start()
    {
        // gets parent for reference
        parentEnemy = transform.parent.gameObject.GetComponent<Enemy>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // if a player enters the collider, damage it
        if (other.gameObject.GetComponent<PlayerBehavior>() != null && !other.gameObject.GetComponent<PlayerBehavior>().invincible)
        {
            PlayerBehavior player = other.gameObject.GetComponent<PlayerBehavior>();
            player.lives--;
            // calculate knockback to player
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
            float vectorX = parentEnemy.knockback * ((player.transform.position.x - transform.position.x) / distance);
            float vectorY = parentEnemy.knockback * ((player.transform.position.y - transform.position.y) / distance);
            // zero out physics forces to prevent erratic motion -- not working tho??? the player can still pass thru the enemy with a running start
            player.rigidBody.velocity = Vector2.zero;
            Debug.Log(player.rigidBody.velocity);
            // apply knockback to player
            player.rigidBody.AddForce(new Vector2(vectorX, vectorY + 0.2f), ForceMode2D.Impulse);
            // make player invincible to prevent several hits in rapid succession
            player.GiveIFrames(player.IFrameTime);
            Debug.Log(player.rigidBody.velocity);
        }
    }
}
