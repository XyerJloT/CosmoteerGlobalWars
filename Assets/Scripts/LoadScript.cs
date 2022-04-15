using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class LoadScript : MonoBehaviourPunCallbacks
{
    public Text text;
    void Start()
    {
        text.text = "���������...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        text.text = "���������!";
        print("���������");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        text.text = "���������������...";
    }
}
