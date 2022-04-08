using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShipsSpawner : MonoBehaviour
{
    public GameObject[] ships;
    PhotonView view;
    public static bool droneSpawning, fregatSpawning, esminetsSpawning, crayserSpawning, linkorSpawning;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
    }
    public void ShipSpawn(int value)
    {
            switch (value)
            {
                case 0:
                droneSpawning = true;
                fregatSpawning = false; esminetsSpawning = false; crayserSpawning = false; linkorSpawning = false;
                //PhotonNetwork.Instantiate(ships[value].name, gameObject.transform.position, Quaternion.identity);
                    break;

                case 1:
                fregatSpawning = true;
                droneSpawning = false; esminetsSpawning = false; crayserSpawning = false; linkorSpawning = false;
                //PhotonNetwork.Instantiate(ships[value].name, gameObject.transform.position, Quaternion.identity);
                break;

                case 2:
                esminetsSpawning = true;
                fregatSpawning = false; droneSpawning = false; crayserSpawning = false; linkorSpawning = false;
                //PhotonNetwork.Instantiate(ships[value].name, gameObject.transform.position, Quaternion.identity);
                break;

                case 3:
                crayserSpawning = true;
                fregatSpawning = false; esminetsSpawning = false; droneSpawning = false; linkorSpawning = false;
                //PhotonNetwork.Instantiate(ships[value].name, gameObject.transform.position, Quaternion.identity);
                break;

                case 4:
                droneSpawning = false; fregatSpawning = false; esminetsSpawning = false; crayserSpawning = false; linkorSpawning = false;
                break;
            }
    }

    public void ShipInstantiate(Transform pos)
    {
        if(droneSpawning) PhotonNetwork.Instantiate(ships[0].name, pos.position, Quaternion.identity);
        if(fregatSpawning) PhotonNetwork.Instantiate(ships[1].name, pos.position, Quaternion.identity);
        if(esminetsSpawning) PhotonNetwork.Instantiate(ships[2].name, pos.position, Quaternion.identity);
        if(crayserSpawning) PhotonNetwork.Instantiate(ships[3].name, pos.position, Quaternion.identity);
    }
}
