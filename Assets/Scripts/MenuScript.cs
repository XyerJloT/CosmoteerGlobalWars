using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuScript : MonoBehaviourPunCallbacks
{
    public InputField  nickNameField;
    [SerializeField] private Slider _sliderHeight, _sliderWeight;

    private string nickName, randomCode;
    private int isAssigned;
    private void Start()
    {
        string nickName = PlayerPrefs.GetString("NickName");
        randomCode = PlayerPrefs.GetString("RandomCode");
        isAssigned = PlayerPrefs.GetInt("IsAssigned");

        nickNameField.text = nickName;
    }

<<<<<<< Updated upstream
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
=======
    public void CreateRoom(bool redTeam)
>>>>>>> Stashed changes
    {
        nickName = nickNameField.text;
        if (isAssigned == 0) randomCode = "#" + Random.Range(1000, 9999);
        PhotonNetwork.NickName = nickName + randomCode;
        isAssigned = 1;

        PlayerPrefs.SetString("NickName", nickName);
        PlayerPrefs.SetString("RandomCode", randomCode);
        PlayerPrefs.SetInt("IsAssigned", isAssigned);

<<<<<<< Updated upstream
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
=======
        MapSpawner._height = Mathf.RoundToInt(_sliderHeight.value);
        MapSpawner._weight = Mathf.RoundToInt(_sliderWeight.value);
>>>>>>> Stashed changes

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom($"{PhotonNetwork.NickName}'s room", roomOptions);
        if(redTeam) TeamManager.playerTeam = 1;
        else TeamManager.playerTeam = 2;
    }

<<<<<<< Updated upstream
    public void JoinBlueRoom() //игрок подключается к синей
=======
    public void JoinRoom(bool redTeam)
>>>>>>> Stashed changes
    {
        nickName = nickNameField.text;
        if (isAssigned == 0) randomCode = "#" + Random.Range(1000, 9999);
        PhotonNetwork.NickName = nickName + randomCode;
        isAssigned = 1;

<<<<<<< Updated upstream


=======
>>>>>>> Stashed changes
        PlayerPrefs.SetString("NickName", nickName);
        PlayerPrefs.SetString("RandomCode", randomCode);
        PlayerPrefs.SetInt("IsAssigned", isAssigned);

<<<<<<< Updated upstream
        PhotonNetwork.JoinRoom(joinField.text);
        TeamManager.playerTeam = 2;
=======
        PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.JoinRoom(joinField.text);
        if (redTeam) TeamManager.playerTeam = 1;
        else TeamManager.playerTeam = 2;
>>>>>>> Stashed changes
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
