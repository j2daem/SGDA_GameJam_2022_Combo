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
    // array represents current order's ingredients, using 1 to indicate an ingredient is present and 0 to indicate it is not
    // in order, ingredients are [lettuce, cheese, onion, tomato, pickle, meat]
    public int[] currentOrder;
    
    void Start()
    {
        // gets new randomization seed and initializes array
        Random.InitState((int) System.DateTime.Now.Ticks);
        currentOrder = new int[] {0, 0, 0, 0, 0, 0};

        GenerateOrder();
    }

    void GenerateOrder()
    {
        // randomize each ingredient
        for (int i = 0; i < 5; i++)
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
        Debug.Log(currentOrder[0] + ", " + currentOrder[1] + ", " + currentOrder[2] + ", " + currentOrder[3] + ", " + currentOrder[4] + ", " + currentOrder[5]);
    }
}
