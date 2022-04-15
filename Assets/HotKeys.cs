using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class HotKeys : MonoBehaviour
{
    private bool _alreadyEscape;
    [SerializeField] GameObject[] exitUI;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) ExitMenuProcesses();
    }

    public void ExitMenuProcesses()
    {
        if (!_alreadyEscape)
        {
            foreach (var item in exitUI)
            {
                item.SetActive(true);
            }
            _alreadyEscape = true;
        }

        else
        {
            foreach (var item in exitUI)
            {
                item.SetActive(false);
            }
            _alreadyEscape = false;
        }

    }

    public void Exit()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
