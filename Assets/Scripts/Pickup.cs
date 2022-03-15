// Author:          Scott Krabbenhoft
// Created on:      03/15/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/15/2022

// Description:     For testing with combo system. May be helpful if we decide to have ingredients as drops from defeated enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType {lettuce, cheese, onion, tomato, pickle, meat};

public class Pickup : MonoBehaviour
{
    [SerializeField] IngredientType type;

    PlayerBehavior player;
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case IngredientType.lettuce:
                sprite.color = new Color(0.45f, 0.92f, 0.38f, 1.0f);
                break;
            case IngredientType.cheese:
                sprite.color = Color.yellow;
                break;
            case IngredientType.onion:
                sprite.color = Color.white;
                break;
            case IngredientType.tomato:
                sprite.color = Color.red;
                break;
            case IngredientType.pickle:
                sprite.color = new Color(0.05f, 0.35f, 0.12f, 1.0f);
                break;
            case IngredientType.meat:
                sprite.color = new Color(0.22f, 0.17f, 0.12f, 1.0f);
                break;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<PlayerBehavior>();
        if (player != null)
        {
            player.CollectIngredient(type);
            Destroy(gameObject);
        }
    }
}
