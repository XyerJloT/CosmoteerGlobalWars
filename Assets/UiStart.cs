using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStart : MonoBehaviour
{
    public GameObject _menu;
    public GameObject _back;
    //List<Vector2> scale;
    Vector2 scale, scale2, scale3;
    private void Start()
    {
        scale = _menu.GetComponent<RectTransform>().sizeDelta;
        scale2 = _back.GetComponent<RectTransform>().sizeDelta;
        scale3 = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    public void ScaleUI()
    {
        _menu.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        _back.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        _menu.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        StartCoroutine("ScaleCoroutine") ;
    }

    private IEnumerator ScaleCoroutine()
    {
        _back.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _back.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            _back.GetComponent<RectTransform>().sizeDelta += new Vector2(scale2.x / 10, scale2.y / 10);
            _menu.GetComponent<RectTransform>().sizeDelta += new Vector2(scale.x/10, scale.y/10);
            gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale3.x / 10, scale3.y / 10);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        _menu.GetComponent<UiMenu>().MenuStart();
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        gameObject.GetComponent<RectTransform>().sizeDelta = scale3;
        gameObject.SetActive(false);
    }
}
