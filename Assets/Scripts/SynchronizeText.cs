using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SynchronizeText : MonoBehaviour
{
     PhotonView view;
     private InputField thisField;
     private void Start()
     {
         view = gameObject.GetComponent<PhotonView>();
         thisField = gameObject.GetComponent<InputField>(); 
    }


    public void onEndChange()
    {
        view.RPC("UpdateText", RpcTarget.All, thisField.text);
    }

     private void Update()
     {
         //thisField = gameObject.GetComponent<InputField>();
         //gameObject.GetComponent<InputField>().text = globalNum;
    }

    [PunRPC]
    public void UpdateText(string text)
    {
        gameObject.GetComponent<InputField>().text = text;
    }
}
