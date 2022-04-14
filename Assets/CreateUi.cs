using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUi : MonoBehaviour
{
    [SerializeField]UiBack clearView;
    [SerializeField] GameObject _createRed, _createBlue, _joinRed, _joinBlue, _back, _slider1, _slider2, _toggle1;
    private Vector2 scale1, scale2, scale3, scale4, scale5, scaleSlider1, scaleSlider2, scaleToggle1;

    private void Start()
    {
        scale1 = _createRed.GetComponent<RectTransform>().sizeDelta;
        scale2 = _createBlue.GetComponent<RectTransform>().sizeDelta;
        scale3 = _joinRed.GetComponent<RectTransform>().sizeDelta;
        scale4 = _joinBlue.GetComponent<RectTransform>().sizeDelta;
        scale5 = _back.GetComponent<RectTransform>().sizeDelta;
        scaleSlider1 = _slider1.GetComponent<RectTransform>().sizeDelta;
        scaleSlider2 = _slider2.GetComponent<RectTransform>().sizeDelta;
        scaleToggle1 = _toggle1.GetComponent<RectTransform>().sizeDelta;
    }

    public void ClearView(bool creating)
    {
        clearView.Back(false);
        _back.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        _back.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        if (creating)
        {
            _createRed.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _createBlue.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _slider1.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _slider2.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _toggle1.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
        else 
        {
         _joinRed.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
         _joinBlue.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
        StartCoroutine("CoroutineClearView", creating);
    }

    IEnumerator CoroutineClearView(bool creating)
    {
        _back.SetActive(true);
        if (creating)
        {
            _createRed.SetActive(true);
            _createBlue.SetActive(true);
            _slider1.SetActive(true);
            _slider2.SetActive(true);
            _toggle1.SetActive(true);
        }
        else
        {
            _joinRed.SetActive(true);
            _joinBlue.SetActive(true);
        }

        for (int i = 0; i < 10; i++)
        {
            if (creating)
            {
                _createRed.GetComponent<RectTransform>().sizeDelta += new Vector2(scale1.x / 10, scale1.y / 10);
                _createBlue.GetComponent<RectTransform>().sizeDelta += new Vector2(scale2.x / 10, scale2.y / 10);
                _slider1.GetComponent<RectTransform>().sizeDelta += new Vector2(scaleSlider1.x / 10, scaleSlider1.y / 10);
                _slider2.GetComponent<RectTransform>().sizeDelta += new Vector2(scaleSlider2.x / 10, scaleSlider2.y / 10);
                _toggle1.GetComponent<RectTransform>().sizeDelta += new Vector2(scaleToggle1.x / 10, scaleToggle1.y / 10);
            }
            else
            {
                _joinRed.GetComponent<RectTransform>().sizeDelta += new Vector2(scale3.x / 10, scale3.y / 10);
                _joinBlue.GetComponent<RectTransform>().sizeDelta += new Vector2(scale4.x / 10, scale4.y / 10);
            }
            _back.GetComponent<RectTransform>().sizeDelta += new Vector2(scale5.x / 10, scale5.y / 10);
            yield return new WaitForSecondsRealtime(0.008f);
        }
    }
}
