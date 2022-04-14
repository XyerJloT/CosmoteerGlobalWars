using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseShip : MonoBehaviour
{
    public static GameObject[] shipsIcon;

    private void Start()
    {
        shipsIcon = GameObject.FindGameObjectsWithTag("ShipIcon");
    }

    public void DisableOtherIcons()
    {
        for (int i = 0; i < shipsIcon.Length; i++)
        {
            shipsIcon[i].GetComponent<Toggle>().isOn = false;
        }
        gameObject.GetComponent<Toggle>().isOn = true;
    }
}
