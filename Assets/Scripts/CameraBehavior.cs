// Author:          Scott Krabbenhoft
// Created on:      03/13/2022

// Last edited by:  Scott Krabbenhoft
// Last edited on:  03/13/2022

// Description:     Forces attached camera to follow a given player -- should work with multiple players
//                  if we can figure out how to render multiple camera on the same screen at once

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    
    void Update()
    {
        // sets location to the followed object's location
        transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, -10);
    }
}
