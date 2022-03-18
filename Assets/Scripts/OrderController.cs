// Author:          John Mai
// Created on:      03/15/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/17/2022

// Description:     Generates orders for player to complete. If they collect a wrong ingredient ONCE, fail the order;
//                  otherwise, keep checking if the player is 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [Header("Ingredient Point Values")]
    [SerializeField] int lettucePoints = 25;
    [SerializeField] int cheesePoints = 25;
    [SerializeField] int onionPoints = 25;
    [SerializeField] int tomatoPoints = 25;
    [SerializeField] int picklePoints = 25;
    [SerializeField] int meatPoints = 25;

    [Header("Object Initialization")]
    [SerializeField] PlayerBehavior player = null;
    [SerializeField] DisplayOrder displayOrder = null;

    IngredientType[] ingredientsOrdered;
    bool[] ingredientCollected;

    int totalNumberOfIngredients;
    public float combo = 1f;

    private void Start()
    {
        // New randomization seed
        Random.InitState((int)System.DateTime.Now.Ticks);

        // Get first order
        NewOrderTicket();
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NewOrderTicket();
        }
    }
    */

    private void NewOrderTicket()
    {
        // Get total number of items on order -- Random.Range with integers use inclusive min and exclusive max values
        totalNumberOfIngredients = Random.Range(1, 4);

        // Replace array with new array that fits the new total number of items
        System.Array.Resize<IngredientType>(ref ingredientsOrdered, totalNumberOfIngredients);
        System.Array.Resize<bool>(ref ingredientCollected, totalNumberOfIngredients);

        Debug.Log("New order!");

        // Assign ingredients for every spot in the array
        // IS THERE A WAY TO MAKE THE LOG FOR ALL INGREDIENTS SEND IN ONE MESSAGE?
        for (int i = 0; i < ingredientsOrdered.Length; i++)
        {
            ingredientsOrdered[i] = (IngredientType)Random.Range(0, 5);
            ingredientCollected[i] = false;
            Debug.Log("Ingredient = " + ingredientsOrdered[i]);
        }

        displayOrder.DisplayNewOrder(ingredientsOrdered);
    }

    // Call in Pickup class whenever any ingredient / item is picked up
    public void UpdateOrderTicket(IngredientType pickup)
    {
        // Accounts for double ingredients
        bool noMatchFound = true;

        // Check if the ingredient has already been collected
        for (int i = 0; i < totalNumberOfIngredients; i++)
        {
            // Check to see if the ingredient matches and has not already been collected
            if ((ingredientsOrdered[i] == pickup) && (!ingredientCollected[i]))
            {
                ingredientCollected[i] = true;
                noMatchFound = false;

                // Now check if this collected ingredient has completed the order
                if (CheckOrderComplete())
                {
                    CompleteOrder();
                }

                // Exit the loop to avoid pickup 'clearing' duplicate ordered ingredients
                break;
            }
        }

        if (noMatchFound)
        {
            FailOrder();
        }
    }

    private bool CheckOrderComplete()
    {
        bool completed = true;

        // Check if any ingredient hasn't been collected; if missing, the check will return false
        for (int i = 0; i < totalNumberOfIngredients; i++)
        {
            if (!ingredientCollected[i])
            {
                completed = false;
            }
        }
        return completed;
    }

    private void CompleteOrder()
    {
        float score = 100;

        for (int i = 0; i < totalNumberOfIngredients; i++)
        {
            if (ingredientsOrdered[i] == IngredientType.meat)
            {
                score += meatPoints;
            }

            else if (ingredientsOrdered[i] == IngredientType.cheese)
            {
                score += cheesePoints;
            }

            else if(ingredientsOrdered[i] == IngredientType.tomato)
            {
                score += tomatoPoints;
            }

            else if (ingredientsOrdered[i] == IngredientType.lettuce)
            {
                score += lettucePoints;
            }

            else if (ingredientsOrdered[i] == IngredientType.onion)
            {
                score += onionPoints;
            }

            else if (ingredientsOrdered[i] == IngredientType.pickle)
            {
                score += picklePoints;
            }
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

        player.lives = Mathf.Clamp(player.lives + 1, 0, 10);
        player.score += (int)Mathf.Round(score);

        Debug.Log("Order complete. Points awarded " + (int)Mathf.Round(score));
        Debug.Log("Current player lives: " + player.lives + "; current player score: " + player.score);

        NewOrderTicket();
    }

    private void FailOrder()
    {
        combo = 1;
        player.lives = Mathf.Clamp(player.lives - 1, 0, 10);
        Debug.Log("Failed order.");

        NewOrderTicket();
    }
}
