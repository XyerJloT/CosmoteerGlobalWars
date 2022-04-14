using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBack : MonoBehaviour
{
    public GameObject _menu, _start, _cBt, _cBt2, _jBt, _jBt2, _slider1, _slider2, _toggle1;
    Vector2 scale, scale2, scale3, scaleB1, scaleB2, scaleB3, scaleB4, scaleSlider1, scaleSlider2, scaleToggle1;
    private void Start()
    {
        scale = _menu.GetComponent<RectTransform>().sizeDelta;
        scale2 = _start.GetComponent<RectTransform>().sizeDelta;
        scale3 = gameObject.GetComponent<RectTransform>().sizeDelta;
        scaleB1 = _cBt.GetComponent<RectTransform>().sizeDelta;
        scaleB2 = _cBt2.GetComponent<RectTransform>().sizeDelta;
        scaleB3 = _jBt.GetComponent<RectTransform>().sizeDelta;
        scaleB4 = _jBt2.GetComponent<RectTransform>().sizeDelta;
        scaleSlider1 = _slider1.GetComponent<RectTransform>().sizeDelta;
        scaleSlider2 = _slider2.GetComponent<RectTransform>().sizeDelta;
        scaleToggle1 = _toggle1.GetComponent<RectTransform>().sizeDelta;
    }

    public void Back(bool needStart)
    {
        if (needStart)
        {
            _start.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _start.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
            _cBt.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleB1.x / 10, scaleB1.y / 10);
            _cBt2.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleB2.x / 10, scaleB2.y / 10);
            _jBt.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleB3.x / 10, scaleB3.y / 10);
            _jBt2.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleB4.x / 10, scaleB4.y / 10);
            _slider1.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleSlider1.x / 10, scaleSlider1.y / 10);
            _slider2.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleSlider2.x / 10, scaleSlider2.y / 10);
            _toggle1.GetComponent<RectTransform>().sizeDelta -= new Vector2(scaleToggle1.x / 10, scaleToggle1.y / 10);
            yield return new WaitForSecondsRealtime(0.003f);
        }
        _cBt.SetActive(false);
        _cBt2.SetActive(false);
        _jBt.SetActive(false);
        _jBt2.SetActive(false);
        _slider1.SetActive(false);
        _slider2.SetActive(false);
        _toggle1.SetActive(false);

        _cBt.GetComponent<RectTransform>().sizeDelta = scaleB1;
        _cBt2.GetComponent<RectTransform>().sizeDelta = scaleB2;
        _jBt.GetComponent<RectTransform>().sizeDelta = scaleB3;
        _jBt2.GetComponent<RectTransform>().sizeDelta = scaleB4;
        _slider1.GetComponent<RectTransform>().sizeDelta = scaleSlider1;
        _slider2.GetComponent<RectTransform>().sizeDelta = scaleSlider2;
        _toggle1.GetComponent<RectTransform>().sizeDelta = scaleToggle1;
    }

    IEnumerator CoroutineBack(bool needStart)
    {
        _menu.GetComponent<UiMenu>().MenuClose();
        for (int i = 0; i < 10; i++)
        {
            _menu.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale.x / 10, scale.y / 10);
            if(needStart) _start.GetComponent<RectTransform>().sizeDelta += new Vector2(scale2.x / 10, scale2.y / 10);
            gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale3.x / 10, scale3.y / 10);
            yield return new WaitForSecondsRealtime(0.003f);
        }
        _menu.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        _menu.GetComponent<RectTransform>().sizeDelta = scale;
        gameObject.GetComponent<RectTransform>().sizeDelta = scale3;
        gameObject.SetActive(false);
    }
}
