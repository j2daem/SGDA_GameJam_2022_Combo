// Author:          Scott Krabbenhoft
// Created on:      03/13/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/15/2022

// Description:     Generates orders for player to complete

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [Header("Ingredient Point Values")]
    [SerializeField] int lettucePoints = 25;
    [SerializeField] int cheesePoints = 25;
    [SerializeField] int onionPoints = 25;
    [SerializeField] int tomatoPoints = 25;
    [SerializeField] int picklePoints = 25;
    [SerializeField] int meatPoints = 25;

    [Header("Player")]
    [SerializeField] PlayerBehavior player;

    int combo = 1;
    
    // solution array represents current order's ingredients, using 1 to indicate an ingredient is present and 0 to indicate it is not
    // in order, ingredients are [lettuce, cheese, onion, tomato, pickle, meat]
    public int[] currentOrder;
    // progress array represents current correct ingredients, using 1 to indicate an ingredient is correct (whether present or not) and any other number to indicate it is not
    // in order, ingredients are [lettuce, cheese, onion, tomato, pickle, meat]
    public int[] orderProgress;
    
    void Start()
    {
        // gets new randomization seed and generates first order
        Random.InitState((int) System.DateTime.Now.Ticks);

        GenerateOrder();
    }

    void GenerateOrder()
    {
        // initializes arrays, or resets them to 0 if they have been previously modified
        currentOrder = new int[] {0, 0, 0, 0, 0, 0};
        orderProgress = new int[] {0, 0, 0, 0, 0, 0};
        
        // randomize each ingredient
        for (int i = 0; i <= 5; i++)
        {
            currentOrder[i] = Random.Range(0, 2);
        }

        // if no ingredients were selected, choose one and fill it
        if (currentOrder[0] == 0 &&
            currentOrder[1] == 0 &&
            currentOrder[2] == 0 &&
            currentOrder[3] == 0 &&
            currentOrder[4] == 0 &&
            currentOrder[5] == 0)
        {
            currentOrder[Random.Range(0, 6)] = 1;
        }

        // writes order to console for testing purposes
        PrintArrays();
    }

    void Update()
    {
        // if the correct number of all ingredients has been collected, complete the order
        if (orderProgress[0] == currentOrder[0] &&
            orderProgress[1] == currentOrder[1] &&
            orderProgress[2] == currentOrder[2] &&
            orderProgress[3] == currentOrder[3] &&
            orderProgress[4] == currentOrder[4] &&
            orderProgress[5] == currentOrder[5])
        {
            Debug.Log("Order completed");
            player.score += CompleteOrder();
            Debug.Log(player.score);
        }
        // if too many of any ingredient has been collected, fail the order
        else if (orderProgress[0] > currentOrder[0] ||
            orderProgress[1] > currentOrder[1] ||
            orderProgress[2] > currentOrder[2] ||
            orderProgress[3] > currentOrder[3] ||
            orderProgress[4] > currentOrder[4] ||
            orderProgress[5] > currentOrder[5])
        {
            FailOrder();
        }
    }

    int CompleteOrder()
    {
        float score = 100;
        if (currentOrder[0] == 1)
        {
            score += lettucePoints;
        }
        if (currentOrder[1] == 1)
        {
            score += cheesePoints;
        }
        if (currentOrder[2] == 1)
        {
            score += onionPoints;
        }
        if (currentOrder[3] == 1)
        {
            score += tomatoPoints;
        }
        if (currentOrder[4] == 1)
        {
            score += picklePoints;
        }
        if (currentOrder[5] == 1)
        {
            score += meatPoints;
        }

        switch (combo)
        {
            case 1:
                score *= 1.0f;
                break;
            case 2:
                score *= 1.25f;
                break;
            case 3:
                score *= 1.5f;
                break;
            case 4:
                score *= 1.75f;
                break;
            case 5:
                score *= 2.0f;
                break;
        }
        combo = Mathf.Clamp(combo + 1, 1, 5);

        GenerateOrder();
        player.lives = Mathf.Clamp(player.lives + 1, 0, 10);
        return (int) Mathf.Round(score);
    }

    void FailOrder()
    {
        combo = 1;
        GenerateOrder();
        player.lives = Mathf.Clamp(player.lives - 1, 0, 10);
    }

    public void PrintArrays()
    {
        Debug.Log(currentOrder[0] + ", " + currentOrder[1] + ", " + currentOrder[2] + ", " + currentOrder[3] + ", " + currentOrder[4] + ", " + currentOrder[5]);
    }
}
