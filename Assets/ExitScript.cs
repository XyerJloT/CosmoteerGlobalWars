using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{

    public void OnClick()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
