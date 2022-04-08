using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TeamManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static int redIncome = 0, redBudget = 200, blueIncome = 0, blueBudget = 200; //деньги и доход
    public static int playerTeam = 0; //1 = red, 2 = blue, 0 = dev;

    [Header("Поля для ввода")]
    public InputField plus, minus, redPlus, redMinus, bluePlus, blueMinus; //поля для ввода
    private bool redOperation, blueOperation; //для определения того, кому надо прибавлять деньги

    [Header("Объекты для активации")]
    public GameObject[] redObjectsToActivate; //для красных
    public GameObject[] blueObjectsToActivate; //для синих
    public GameObject[] hostObjectsToActivate; //для хоста во избежания странных багов с синхронизацией
    private GameObject[] objectsToActivate; //для разраба

    private GameObject[] stars;
    public List<string> names;

    private GameObject[] ships; //корабли
    private GameObject[] hummersToDisable; //молоточки, которые надо выключить, чтобы показать, что корабль не строится
    private bool needToDestroy = false; //логика для уничтожения молоточков на всех клиентах
    private bool needToChangeColor = false; //логика для смены цвета на всех клиентах


    private int number; //сколько нужно прибавлять вручную

    [Header("Переключатели для следующего хода")]
    public Toggle redTg, blueTg; //переключатели для след хода

    [Header("Список игроков")]
    public Text playerList; //список игроков

    private int i1, i2; //для циклов

    PhotonView view;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerList.text = null;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            int h = PhotonNetwork.PlayerList.Length;
            if (PhotonNetwork.PlayerList[i].ActorNumber == h) playerList.text += PhotonNetwork.PlayerList[i].NickName + " ";
            else playerList.text += PhotonNetwork.PlayerList[i].NickName + ", ";
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].GetComponent<StarScript>().view.RPC("SendText", RpcTarget.All, stars[i].GetComponent<StarScript>().income.GetComponent<Text>().text);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerList.text = null;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            int h = PhotonNetwork.PlayerList.Length;
            if (PhotonNetwork.PlayerList[i].ActorNumber == h) playerList.text += PhotonNetwork.PlayerList[i].NickName + " ";
            else playerList.text += PhotonNetwork.PlayerList[i].NickName + ", ";
        }
    }

    private void Update()
    {
        //playerList.text = PhotonNetwork.PlayerList;


        //молоты на карте
        hummersToDisable = GameObject.FindGameObjectsWithTag("Hummer");

        //корабли на карте
        ships = GameObject.FindGameObjectsWithTag("Ship");

        if (PhotonNetwork.IsMasterClient)
        {
            view.RPC("MoneySynch", RpcTarget.Others, redBudget, redIncome, blueBudget, blueIncome);
            view.RPC("DisableHummers", RpcTarget.All, needToDestroy);
            view.RPC("ChangeShipColor", RpcTarget.All, needToChangeColor);
        }
        if(redTg.isOn && blueTg.isOn && view.IsMine) NextTurn();
    }


    [PunRPC]
    public void DisableHummers(bool destroy)
    {
        if (destroy)
        {
            for (int i = 0; i < hummersToDisable.Length; i++)
            {
                hummersToDisable[i].SetActive(false);
            }
            needToDestroy = false;
        }
    }

    [PunRPC]
    public void ChangeShipColor(bool change)
    {
        if (change)
        {
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i].GetComponent<MoveObject>().isBuilding = false; //изменить цвет
            }
            needToChangeColor = false;
        }
    }

    public void ActivateBlue()
    {
        i2++;
            if (i2 == 1)
        {
            blueTg.isOn = true;
            view.RPC("ToggleSynch", RpcTarget.MasterClient, redTg.isOn, blueTg.isOn);
        }
            if (i2 >= 2)
        {
            blueTg.isOn = false;
            view.RPC("ToggleSynch", RpcTarget.MasterClient, redTg.isOn, blueTg.isOn);
            i2 = 0;
        }
    }

    public void ActivateRed()
    {
        i1++;
        if (i1 == 1)
        {
            redTg.isOn = true;
            view.RPC("ToggleSynch", RpcTarget.MasterClient, redTg.isOn, blueTg.isOn);
        }
        if (i1 >= 2)
        {
            redTg.isOn = false;
            view.RPC("ToggleSynch", RpcTarget.MasterClient, redTg.isOn, blueTg.isOn);
            i1 = 0;
        }
    }

    public void onToggleValueChange()
    {
        if(PhotonNetwork.IsMasterClient) view.RPC("ToggleSynch", RpcTarget.Others, redTg.isOn, blueTg.isOn);
    }

    private void Start() //активировать объекты, которые будут видны только хосту
    {
        playerList.text = null;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playerList.text += PhotonNetwork.PlayerList[i].NickName;
        }

        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)//активация штук для мастера, чтобы избежать багов
        {

            for (int i = 0; i < hostObjectsToActivate.Length; i++)
            {
                hostObjectsToActivate[i].SetActive(true);
            }

        }

        stars = GameObject.FindGameObjectsWithTag("Star");
        for (int i = 0; i < stars.Length; i++)
        {
            int random = Random.Range(0, names.Count);
            stars[i].GetComponent<StarScript>().income.GetComponent<Text>().text = "Доход: " + stars[i].GetComponent<StarScript>().starIncome + " 000$" + " Название:" + names[random] + "-" + Random.Range(0,9);
            names.RemoveAt(random);
        }

        switch (playerTeam)
            {
                case 0:
                    for (int i = 0; i < objectsToActivate.Length; i++)
                    {
                        objectsToActivate[i].SetActive(true);
                    }
                    break;

                case 1:
                    for (int i = 0; i < redObjectsToActivate.Length; i++)
                    {
                        redObjectsToActivate[i].SetActive(true);
                    }
                    break;

                case 2:
                    for (int i = 0; i < blueObjectsToActivate.Length; i++)
                    {
                        blueObjectsToActivate[i].SetActive(true);
                    }
                    break;
            }
        
    }

    public void NextTurn()//следующий ход
    {
        redTg.isOn = false;
        blueTg.isOn = false;
            redBudget += redIncome;
            blueBudget += blueIncome;
        needToChangeColor = true;
        needToDestroy = true;
    }

    public void onValueChange(int x)//кому прибавлять
    {
        if (view.IsMine)
        {
            switch (x)
            {
                case 0:
                    blueOperation = false;
                    redOperation = true;
                    break;
                case 1:
                    redOperation = false;
                    blueOperation = true;
                    break;
                case 2:
                    redOperation = true;
                    blueOperation = true;
                    break;
            }
        }
    }

    //КРАСНЫЕ

    public void MoneyRedPlus()//прибавить деньги красным
    {
        if (view.IsMine)
        {
            MoneyOperations(1);
        }
        else
        {
            view.RPC("MoneyOperations", RpcTarget.MasterClient, 1); //передаём хосту с клиента
        }
    }
    public void MoneyRedMinus()//убавить деньги красным
    {
        if (view.IsMine)
        {
            MoneyOperations(2);
        }
        else
        {
            view.RPC("MoneyOperations", RpcTarget.MasterClient, 2);
        }
    }

    //СИНИЕ

    public void MoneyBluePlus()//прибавить деньги синим
    {
        if (view.IsMine)
        {
            MoneyOperations(3);
        }
        else
        {
            view.RPC("MoneyOperations", RpcTarget.MasterClient, 3);
        }
    }
    public void MoneyBlueMinus()//убавить деньги синим
    {
        if (view.IsMine)
        {
            MoneyOperations(4);
        }
        else
        {
            view.RPC("MoneyOperations", RpcTarget.MasterClient, 4);
        }
    }

    [PunRPC]

    public void MoneyOperations(int i)
    {
        switch (i)
        {
            case 1:
                number = int.Parse(redPlus.text); //прибваить красным
                redBudget += number;
                break;
            case 2:
                number = int.Parse(redMinus.text); //убавить красным
                redBudget -= number;
                break;

            case 3:
                number = int.Parse(bluePlus.text); //прибваить синим
                blueBudget += number;
                break;
            case 4:
                number = int.Parse(blueMinus.text); //убавить синим
                blueBudget -= number;
                break;
        }
    }

    [PunRPC]
    public void MoneySynch(int rBg, int rIn, int bBg, int bIn)
    {
        redBudget = rBg;
        redIncome = rIn;
        blueBudget = bBg;
        blueIncome = bIn;
    }

    [PunRPC]
    public void ToggleSynch(bool rOn, bool bOn)
    {
        redTg.isOn = rOn;
        blueTg.isOn = bOn;
    }

    //СИНХРОНИЗАЦИЯ

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(redIncome);
            stream.SendNext(redBudget);
            stream.SendNext(blueIncome);
            stream.SendNext(blueBudget);

            stream.SendNext(needToDestroy);
            stream.SendNext(needToChangeColor);
        }
        else
        {
            redIncome = (int)stream.ReceiveNext();
            redBudget = (int)stream.ReceiveNext();
            blueIncome = (int)stream.ReceiveNext();
            blueBudget = (int)stream.ReceiveNext();

            needToDestroy = (bool)stream.ReceiveNext();
            needToChangeColor = (bool)stream.ReceiveNext();
        }
    }
}
