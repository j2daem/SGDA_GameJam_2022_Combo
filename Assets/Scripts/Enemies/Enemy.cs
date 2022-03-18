// Author:          John Mai
// Created on:      03/16/2022

// Last edited by: John Mai
// Last edited on:  03/18/2022
// Last edits made: Moved speed value from child classes; added functions to stop and set enemy speed

// Description:     Provides basic enemy behaviors and framework

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType {meat, cheese, tomato, lettuce, onion, pickle};

public class Enemy : MonoBehaviour
{
    [Header("Basic Enemy Settings")]
    public IngredientType type;
    [SerializeField] float health = 100f;
    [SerializeField] float stunDuration = .1f;
    [SerializeField] float waitDuration = .1f;
    public float knockback = 4f;
    public float speed = 0;
    //[SerializeField] GameObject deathEfect = null;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public float GetStunDuration()
    {
        return stunDuration;
    }

    public float GetWaitDuration()
    {
        return waitDuration;
    }

    public void SetSpeed(float speedToSet)
    {
        speed = speedToSet;
    }
}

