// Author:          John Mai
// Created on:      03/16/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/17/2022

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
    public float knockback = 4f;
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
}

