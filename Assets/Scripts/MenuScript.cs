using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuScript : MonoBehaviourPunCallbacks
{
    public InputField createField, joinField, nickNameField;

    public GameObject redTeamCreateJoinButton, blueTeamCreateJoinButton, redTeamJoinButton, blueTeamJoinButton;
    [SerializeField]
    private GameObject[] objectsToDeactivate; //убрать ненужный UI

    private string nickName, randomCode;
    private int isAssigned;
    private void Start()
    {
        string nickName = PlayerPrefs.GetString("NickName");
        randomCode = PlayerPrefs.GetString("RandomCode");
        isAssigned = PlayerPrefs.GetInt("IsAssigned");

        nickNameField.text = nickName;
        objectsToDeactivate = GameObject.FindGameObjectsWithTag("Deactivate");
    }
    public void UIActivateHost()
    {
        for (int i = 0; i < objectsToDeactivate.Length; i++)
        {
            objectsToDeactivate[i].gameObject.SetActive(false);
        }
        //для создателя
        redTeamCreateJoinButton.gameObject.SetActive(true);
        blueTeamCreateJoinButton.gameObject.SetActive(true);
    }

    public void UIActivatePlayer()
    {
        for (int i = 0; i < objectsToDeactivate.Length; i++)
        {
            objectsToDeactivate[i].gameObject.SetActive(false);
        }
        //для игрока
        redTeamJoinButton.gameObject.SetActive(true);
        blueTeamJoinButton.gameObject.SetActive(true);
    }



    public void CreateRedRoom() //хост подключается к красной
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
        PhotonNetwork.CreateRoom(createField.text, roomOptions);
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.D)) TeamManager.playerTeam = 0; //зайти как разработчик
        else TeamManager.playerTeam = 1;
        
    }

    public void CreateBlueRoom() //хост подключается к синей
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
        PhotonNetwork.CreateRoom(createField.text, roomOptions);
        TeamManager.playerTeam = 2;
    }


    public void JoinRedRoom() //игрок подключается к красной
    {
        nickName = nickNameField.text;
        if (isAssigned == 0) randomCode = "#" + Random.Range(1000, 9999);
        PhotonNetwork.NickName = nickName + randomCode;
        isAssigned = 1;



        PlayerPrefs.SetString("NickName", nickName);
        PlayerPrefs.SetString("RandomCode", randomCode);
        PlayerPrefs.SetInt("IsAssigned", isAssigned);

        PhotonNetwork.JoinRoom(joinField.text);
        TeamManager.playerTeam = 1;
    }

    public void JoinBlueRoom() //игрок подключается к синей
    {
        nickName = nickNameField.text;
        if (isAssigned == 0) randomCode = "#" + Random.Range(1000, 9999);
        PhotonNetwork.NickName = nickName + randomCode;
        isAssigned = 1;



        PlayerPrefs.SetString("NickName", nickName);
        PlayerPrefs.SetString("RandomCode", randomCode);
        PlayerPrefs.SetInt("IsAssigned", isAssigned);

        PhotonNetwork.JoinRoom(joinField.text);
        TeamManager.playerTeam = 2;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
