using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
