using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerInput : MonoBehaviour
{
    [SerializeField] GameObject _exit;
    private bool _getDown;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_getDown)
        {
            _exit.SetActive(true);
            _getDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _getDown) 
        {
            _exit.SetActive(false);
            _getDown = false;
        }
    }
}
