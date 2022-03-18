using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] PlayerBehavior player;
    [SerializeField] Text scoreContainer;

    private void Update()
    {
        scoreContainer.text = player.score.ToString();
    }
}
