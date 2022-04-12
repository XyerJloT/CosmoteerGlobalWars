using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class RoomSpawner : MonoBehaviourPunCallbacks
{
    public GameObject _prefab;
    public Transform _parent;
}
