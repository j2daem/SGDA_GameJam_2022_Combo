// Author:          John Mai
// Created on:      03/15/2022

// Last edited by:  John Mai
// Last edited on:  03/16/2022

// Description:     Placeholder to display menu orders

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOrder : MonoBehaviour
{
    [Header("Icon Display Settings")]
    [SerializeField] int totalIcons = 6;

    [Header("Order Icon Initialization")]
    [SerializeField] Image[] orderIcons = new Image[6];
    [SerializeField] Sprite[] ingredientSprites = new Sprite[6];

    public void DisplayNewOrder(IngredientType[] ingredientsOrdered)
    {
        EnableAllIcons();

        // Look through the entire array of ordered ingredients. Change the sprite for the corresponding icon based on the ingredient type
        for (int i = 0; i < ingredientsOrdered.Length; i++)
        {
            switch (ingredientsOrdered[i])
            {
                case IngredientType.meat:
                    orderIcons[i].sprite = ingredientSprites[0];
                    break;

                case IngredientType.cheese:
                    orderIcons[i].sprite = ingredientSprites[1];
                    break;

                case IngredientType.tomato:
                    orderIcons[i].sprite = ingredientSprites[2];
                    break;

                case IngredientType.lettuce:
                    orderIcons[i].sprite = ingredientSprites[3];
                    break;

                case IngredientType.onion:
                    orderIcons[i].sprite = ingredientSprites[4];
                    break;

                case IngredientType.pickle:
                    orderIcons[i].sprite = ingredientSprites[5];
                    break;
            }
        }

        DisableUnusedIcons(ingredientsOrdered.Length);
    }

    private void EnableAllIcons()
    {
        // Enable all of the icons (will turn off unused ones later)
        for (int i = 0; i < totalIcons; i++)
        {
            orderIcons[i].enabled = true;
        }
    }

    public void DisableUnusedIcons(int orderLength)
    {
        for (int i = totalIcons - 1; i >= orderLength; i--)
        {
            orderIcons[i].enabled = false;
        }
    }
}
