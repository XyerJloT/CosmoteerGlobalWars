using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandModToggle : MonoBehaviour
{
    public void OnPress(bool isOn)
    {
        if (isOn) ManageMap._isIlandMode = true;
        else ManageMap._isIlandMode = false;
    }
}
