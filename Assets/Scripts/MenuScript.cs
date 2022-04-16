using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class MenuScript : MonoBehaviourPunCallbacks
{
    public InputField nickNameField;

    private string nickName, randomCode;
    private int isAssigned;
    private void Start()
    {
        string nickName = PlayerPrefs.GetString("NickName");
        randomCode = PlayerPrefs.GetString("RandomCode");
        isAssigned = PlayerPrefs.GetInt("IsAssigned");

        nickNameField.text = nickName;
    }

    public void CreateRoom(bool isRed)
    {
        nickName = nickNameField.text;
        if (isAssigned == 0) randomCode = "#" + Random.Range(1000, 9999);
        PhotonNetwork.NickName = nickName + randomCode;
        isAssigned = 1;

        PlayerPrefs.SetString("NickName", nickName);
        PlayerPrefs.SetString("RandomCode", randomCode);
        PlayerPrefs.SetInt("IsAssigned", isAssigned);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom($"{PhotonNetwork.NickName}'s room", roomOptions);

        if (isRed) PhotonNetwork.LocalPlayer.JoinTeam(1);
        else PhotonNetwork.LocalPlayer.JoinTeam(2);
    }

    public void JoinRoom(bool isRed)
    {
        nickName = nickNameField.text;
        if (isAssigned == 0) randomCode = "#" + Random.Range(1000, 9999);
        PhotonNetwork.NickName = nickName + randomCode;
        isAssigned = 1;

        PlayerPrefs.SetString("NickName", nickName);
        PlayerPrefs.SetString("RandomCode", randomCode);
        PlayerPrefs.SetInt("IsAssigned", isAssigned);

        // PhotonNetwork.JoinRoom(joinField.text);
        PhotonNetwork.JoinRandomRoom();
        if (isRed) PhotonNetwork.LocalPlayer.JoinTeam(1);
        else PhotonNetwork.LocalPlayer.JoinTeam(2);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
