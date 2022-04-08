using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveObject : MonoBehaviour
{
    public GameObject cam;
    PhotonView view;
    Vector3 pos;
    float posX, posY;
    public bool isRed, isBlue; //принадлежность
    public bool isBuilding = true; //корабль строится

    public Color objColor; //цвет корабля

    private int droneService = 0, fregatService = 20, esminetsService = 40, crayserService = 80, linkorService = 160;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        view = GetComponent<PhotonView>();
        ShipsService();
    }


    public void ShipsService()
    {
        //Для красных кораблей
        if (gameObject.layer == LayerMask.NameToLayer("Drone") && isRed) TeamManager.redIncome -= droneService;
        if (gameObject.layer == LayerMask.NameToLayer("Fregat") && isRed) TeamManager.redIncome -= fregatService;
        if (gameObject.layer == LayerMask.NameToLayer("Esminets") && isRed) TeamManager.redIncome -= esminetsService;
        if (gameObject.layer == LayerMask.NameToLayer("Crayser") && isRed) TeamManager.redIncome -= crayserService;
        if (gameObject.layer == LayerMask.NameToLayer("Linkor") && isRed) TeamManager.redIncome -= linkorService;

        //Для синих кораблей
        if (gameObject.layer == LayerMask.NameToLayer("Drone") && isBlue) TeamManager.blueIncome -= droneService;
        if (gameObject.layer == LayerMask.NameToLayer("Fregat") && isBlue) TeamManager.blueIncome -= fregatService;
        if (gameObject.layer == LayerMask.NameToLayer("Esminets") && isBlue) TeamManager.blueIncome -= esminetsService;
        if (gameObject.layer == LayerMask.NameToLayer("Crayser") && isBlue) TeamManager.blueIncome -= crayserService;
        if (gameObject.layer == LayerMask.NameToLayer("Linkor") && isBlue) TeamManager.blueIncome -= linkorService;
    }//налог на корабли

    private void OnDestroy()
    {
        //для красных кораблей
        if (gameObject.layer == LayerMask.NameToLayer("Drone") && isRed) TeamManager.redIncome += droneService;
        if (gameObject.layer == LayerMask.NameToLayer("Fregat") && isRed) TeamManager.redIncome += fregatService;
        if (gameObject.layer == LayerMask.NameToLayer("Esminets") && isRed) TeamManager.redIncome += esminetsService;
        if (gameObject.layer == LayerMask.NameToLayer("Crayser") && isRed) TeamManager.redIncome += crayserService;
        if (gameObject.layer == LayerMask.NameToLayer("Linkor") && isRed) TeamManager.redIncome += linkorService;

        //Для синих кораблей
        if (gameObject.layer == LayerMask.NameToLayer("Drone") && isBlue) TeamManager.blueIncome += droneService;
        if (gameObject.layer == LayerMask.NameToLayer("Fregat") && isBlue) TeamManager.blueIncome += fregatService;
        if (gameObject.layer == LayerMask.NameToLayer("Esminets") && isBlue) TeamManager.blueIncome += esminetsService;
        if (gameObject.layer == LayerMask.NameToLayer("Crayser") && isBlue) TeamManager.blueIncome += crayserService;
        if (gameObject.layer == LayerMask.NameToLayer("Linkor") && isBlue) TeamManager.blueIncome += linkorService;
    }//убираем налог после уничтожения кораблей




    private void Update()
    {
        //если строится, то делать его полупрозрачным
        if (isBuilding) gameObject.GetComponent<SpriteRenderer>().color = new Color(objColor.r - 0.362f, objColor.g - 0.2f, objColor.b - 0.362f, 0.9f);

        //если построился, вернуть обычный цвет
        else if (!isBuilding) gameObject.GetComponent<SpriteRenderer>().color = new Color(objColor.r, objColor.g, objColor.b, 1f);


        pos = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    private void OnTriggerStay2D(Collider2D collision)//ставим принадлежность звезды к какой-либо команде при входе
    {
        if (collision.gameObject.CompareTag("Star") && isRed)
        {
            collision.gameObject.GetComponent<StarScript>().isGray = false;
            collision.gameObject.GetComponent<StarScript>().isBlue = false;
            collision.gameObject.GetComponent<StarScript>().isRed = true;         
        }
        if (collision.gameObject.CompareTag("Star") && isBlue)
        {
            collision.gameObject.GetComponent<StarScript>().isGray = false;
            collision.gameObject.GetComponent<StarScript>().isRed = false;
            collision.gameObject.GetComponent<StarScript>().isBlue = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //убираем принадлежность звезды к какой-либо команде
    {
        if (collision.gameObject.CompareTag("Star") && isRed)
        {
            collision.gameObject.GetComponent<StarScript>().isRed = false;
            collision.gameObject.GetComponent<StarScript>().isGray = true;
        }
        if (collision.gameObject.CompareTag("Star") && isBlue)
        {
            collision.gameObject.GetComponent<StarScript>().isBlue = false;
            collision.gameObject.GetComponent<StarScript>().isGray = true;
        }
    }

    private void OnMouseDrag()
    {
        posX = pos.x;
        posY = pos.y;
        if (view.IsMine)
        {
            if (TeamManager.playerTeam == 1 && isRed || TeamManager.playerTeam == 2 && isBlue) //двигать объект могут только люди определённой команды, bool настраивается в инспекторе
            {
                gameObject.transform.position = new Vector3(posX, posY, 10);
            }
        }
        if (Input.GetKey(KeyCode.Delete) && TeamManager.playerTeam == 1 && isRed || Input.GetKey(KeyCode.Delete) && TeamManager.playerTeam == 2 && isBlue) PhotonNetwork.Destroy(gameObject);
    }
}
