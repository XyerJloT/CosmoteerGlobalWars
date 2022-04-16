using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandModToggle : MonoBehaviour
{
    public void OnPress(bool isOn)
    {
        if (isOn) ManageMap._isIslandMode = true;
        else ManageMap._isIslandMode = false;
    }
}
