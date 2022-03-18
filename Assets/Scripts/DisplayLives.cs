// Author:          John Mai
// Created on:      03/16/2022

// Last edited by: John Mai
// Last edited on:  03/18/2022
// Last edits made: XXX

// Description:     UI display for the life count

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLives : MonoBehaviour
{
    [SerializeField] PlayerBehavior player;
    [SerializeField] Text livesContainer;

    private void Update()
    {
        livesContainer.text = player.lives.ToString();
    }
}
