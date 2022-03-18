using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCombo : MonoBehaviour
{
    [SerializeField] OrderController orderController;
    [SerializeField] Text comboContainer;

    private void Update()
    {
        comboContainer.text = orderController.combo.ToString();
    }
}
