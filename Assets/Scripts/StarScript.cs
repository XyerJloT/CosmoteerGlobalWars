using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class StarScript : MonoBehaviour, IPunObservable
{
    [Header("Принадлежность")]
    public bool isBlue = false;
    public bool isRed = false;
    public bool isGray = false;

    [Header("Графика")]
    public GameObject stroke; //обводка
    public GameObject income; //текстовое отображение дохода со звезды

    private int i = 0; //для отсчёта кликов (чтобы менять команду звезде)

    [Header("Доход звезды")]
    public int starIncome = 5; //программный доход

    private bool onceRed, onceBlue, onceGray, wasRed, wasBlue; //штуки для логики

    [Header("Корабли в системе")]
    public GameObject[] shipsAround; //корабли, прикреплённые к звезде

    Transform starPos; //положение звезды на карте

    public PhotonView view;



    private void Start()
    {
       // income.GetComponent<Text>().text = "Доход: " + starIncome + " 000$" + " Название:";
        view = GetComponent<PhotonView>();
        starPos = gameObject.transform;
    }

    [PunRPC]

    public void SendText(string txt)
    {
        income.GetComponent<Text>().text = txt;
    }

    private void Update() //тут меняются цвета
    {

        if (isRed) //причисление к какой-либо команде
            {
            wasRed = true;
                stroke.GetComponent<SpriteRenderer>().color = Color.red;
                if (!onceRed) StartCoroutine("IncomingOnce");
                onceRed = true;
            }
            if (isBlue) //голубая
            {
            wasBlue = true;
                stroke.GetComponent<SpriteRenderer>().color = Color.blue;
                if (!onceBlue) StartCoroutine("IncomingOnce");
                onceBlue = true;
            }
            if (isGray) //ничья
            {
                stroke.GetComponent<SpriteRenderer>().color = Color.clear;
                if (!onceGray) StartCoroutine("IncomingOnce");
                onceGray = true;
            }
        
    }

    private IEnumerator IncomingOnce()
    {
        
            if (isRed && !onceRed)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                if (wasBlue)
                {
                    TeamManager.blueIncome -= starIncome;
                    wasBlue = false;
                }
                TeamManager.redIncome += starIncome;
                onceBlue = false;
                onceGray = false;
            }

            if (isBlue && !onceBlue)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                stroke.GetComponent<SpriteRenderer>().color = Color.clear;
                if (wasRed)
                {
                    TeamManager.redIncome -= starIncome;
                    wasRed = false;
                }
                TeamManager.blueIncome += starIncome;
                onceRed = false;
                onceGray = false;
            }

            if (isGray && !onceGray)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                if (wasBlue)
                {
                    TeamManager.blueIncome -= starIncome;
                    wasBlue = false;
                }
                if (wasRed)
                {
                    TeamManager.redIncome -= starIncome;
                    wasRed = false;
                }
                onceRed = false;
                onceBlue = false;
            }
        
    }

    private void OnMouseEnter()
    {

        {
            stroke.GetComponent<SpriteRenderer>().color = Color.grey; //показывание дохода со звезды и её красивая обводка
            income.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (ShipsSpawner.droneSpawning) //спавн дрона
        {
            GameObject.FindGameObjectWithTag("ShipSpawner").GetComponent<ShipsSpawner>().ShipInstantiate(starPos);
        }

        if (ShipsSpawner.fregatSpawning) //спавн фрегата
        {
            GameObject.FindGameObjectWithTag("ShipSpawner").GetComponent<ShipsSpawner>().ShipInstantiate(starPos);;
        }

        if (ShipsSpawner.esminetsSpawning) //спавн эсминца
        {
            GameObject.FindGameObjectWithTag("ShipSpawner").GetComponent<ShipsSpawner>().ShipInstantiate(starPos);
        }

        if (ShipsSpawner.crayserSpawning) //сван крейсера
        {
            GameObject.FindGameObjectWithTag("ShipSpawner").GetComponent<ShipsSpawner>().ShipInstantiate(starPos);
        }

        if (ShipsSpawner.linkorSpawning) //сван линкора
        {
            GameObject.FindGameObjectWithTag("ShipSpawner").GetComponent<ShipsSpawner>().ShipInstantiate(starPos);
        }
    }

    private void OnMouseExit()
    {

        
            stroke.GetComponent<SpriteRenderer>().color = Color.clear;
            income.SetActive(false);
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isBlue);
            stream.SendNext(isRed);
            stream.SendNext(isGray);
            //stream.SendNext(onceRed);
           // stream.SendNext(onceBlue);
           // stream.SendNext(onceGray);
            stream.SendNext(wasRed);
            stream.SendNext(wasBlue);
        }
        else
        {
            isBlue = (bool)stream.ReceiveNext();
            isRed = (bool)stream.ReceiveNext();
            isGray = (bool)stream.ReceiveNext();
           // onceRed = (bool)stream.ReceiveNext();
           // onceBlue = (bool)stream.ReceiveNext();
           // onceGray = (bool)stream.ReceiveNext();
            wasRed = (bool)stream.ReceiveNext();
            wasBlue = (bool)stream.ReceiveNext();
        }
    }
}
