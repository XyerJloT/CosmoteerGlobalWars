using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class textLogic : MonoBehaviour //IPunObservable
{ 
    public string teamIncome = "Доход красных: ";
    public string teamBudget = " Бюджет красных: ";
    public bool isblue;

    private Text message;

    private void Start()
    {
    }
    void Update()
    {
        if (!isblue) gameObject.GetComponent<Text>().text = teamIncome + TeamManager.redIncome + "K" + teamBudget + TeamManager.redBudget + "K";
        else gameObject.GetComponent<Text>().text = teamIncome + TeamManager.blueIncome + "K" + teamBudget + TeamManager.blueBudget + "K";

        message = gameObject.GetComponent<Text>();
        gameObject.GetComponent<Text>().text = message.text;
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(message);
            stream.SendNext(isblue);
        }
        else
        {
            message = (Text)stream.ReceiveNext();
            isblue = (Text)stream.ReceiveNext();
        }
    }*/
}
