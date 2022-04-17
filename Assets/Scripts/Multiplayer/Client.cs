using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class Client : MonoBehaviour
{

    [SerializeField]PhotonView view;

    public void SendMoneyFromOldToNew() //пригодится, когда передаём только что зашедшим игрокам инфу о команде, или когда наступает след ход
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber < PhotonNetwork.PlayerList[i].ActorNumber &&
                PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == PhotonNetwork.PlayerList[i].GetPhotonTeam().Code)
            {
                view.RPC("GetMoney", RpcTarget.Others, Player.Instance.Balance, Player.Instance.Income, Player.Instance.MyTeam.Id);
            }
        }
    }

    public void SendMoneyOnClientAction() //отправляем новый бюджет и доход с любого клиента всем остальным в его команде. Пригодится при покупке кораблей и проч.
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == PhotonNetwork.PlayerList[i].GetPhotonTeam().Code)
            {
                view.RPC("GetMoney", RpcTarget.Others, Player.Instance.Balance, Player.Instance.Income, PhotonNetwork.LocalPlayer.GetPhotonTeam().Code);
            }
        }
    }

    [PunRPC] private void GetMoney(int _balance, int _income, int _teamID)
    {
        if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == _teamID) //принимаем только от сокомандников
        {
            Player.Instance.Balance = _balance;
            Player.Instance.Income = _income;
        }
    }
}
