using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBack : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects, joinOrCreateObjects;
    private Vector2[] scalesMenu, scalesJoinOrCreateObjects;

    [SerializeField] GameObject _start, _menu;
    Vector2 _startScale;
    private void Start()
    {
        _startScale = _start.GetComponent<RectTransform>().sizeDelta;
        scalesMenu = new Vector2[menuObjects.Length];
        scalesJoinOrCreateObjects = new Vector2[joinOrCreateObjects.Length];
        for (int i = 0; i < menuObjects.Length; i++)
        {
            scalesMenu[i] = menuObjects[i].GetComponent<RectTransform>().sizeDelta;
        }

        for (int i = 0; i < joinOrCreateObjects.Length; i++)
        {
            scalesJoinOrCreateObjects[i] = joinOrCreateObjects[i].GetComponent<RectTransform>().sizeDelta;
        }
    }

    public void Back(bool needStart)
    {
        if (needStart)
        {
            _start.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _start.SetActive(true);
        }
        StartCoroutine("CoroutineBack", needStart);
    }

    public void ClearCreateAndJoinButtons()
    {
        StartCoroutine("ClearButtons");
    }

    IEnumerator ClearButtons()
    {
        for (int i = 0; i < 10; i++)
        {
            FillArray fill = new FillArray();
            fill.FillRectSizeMinus(joinOrCreateObjects, scalesJoinOrCreateObjects);
            yield return new WaitForSecondsRealtime(0.005f);
        }

        foreach (var _obj in joinOrCreateObjects)
        {
            _obj.SetActive(false);
        }
    }

    IEnumerator CoroutineBack(bool needStart)
    {
        _menu.GetComponent<UiMenu>().MenuClose();
        for (int i = 0; i < 10; i++)
        {
            FillArray fill = new FillArray();
            fill.FillRectSizeMinus(menuObjects, scalesMenu);
            if (needStart) _start.GetComponent<RectTransform>().sizeDelta += new Vector2(_startScale.x / 10, _startScale.y / 10);
            yield return new WaitForSecondsRealtime(0.005f);
        }
        foreach (var _obj in menuObjects)
        {
            _obj.SetActive(false);
        }

        for (int i = 0; i < menuObjects.Length; i++)
        {
            menuObjects[i].GetComponent<RectTransform>().sizeDelta = scalesMenu[i];
        }
    }
}