using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiMenu : MonoBehaviour
{
    public GameObject obj1, obj2, obj3, obj4;
    private Vector2 scale, scale2, scale3, scale4;
    private void Start()
    {
        scale = obj1.GetComponent<RectTransform>().sizeDelta;
        scale2 = obj2.GetComponent<RectTransform>().sizeDelta;
        scale3 = obj3.GetComponent<RectTransform>().sizeDelta;
        scale4 = obj4.GetComponent<RectTransform>().sizeDelta;
    }
    public void MenuStart()
    {
        obj1.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        obj2.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        obj3.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        obj4.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        obj4.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine("CoroutineStart");
    }

    IEnumerator CoroutineStart()
    {
        obj1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        obj2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        obj1.SetActive(true);
        obj2.SetActive(true);
        obj3.SetActive(true);
        obj4.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            obj1.GetComponent<RectTransform>().sizeDelta += new Vector2(scale.x / 10, scale.y / 10);
            obj2.GetComponent<RectTransform>().sizeDelta += new Vector2(scale2.x / 10, scale2.y / 10);
            obj3.GetComponent<RectTransform>().sizeDelta += new Vector2(scale3.x / 10, scale3.y / 10);
            obj4.GetComponent<RectTransform>().sizeDelta += new Vector2(scale4.x / 10, scale4.y / 10);
            yield return new WaitForSecondsRealtime(0.011f);
        }
    }

    public void MenuClose()
    {
        StartCoroutine("CoroutineClose");
    }

    IEnumerator CoroutineClose()
    {
        obj4.transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            obj1.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale.x / 10, scale.y / 10);
            obj2.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale2.x / 10, scale2.y / 10);
            obj3.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale3.x / 10, scale3.y / 10);
            obj4.GetComponent<RectTransform>().sizeDelta -= new Vector2(scale4.x / 10, scale4.y / 10);
            yield return new WaitForSecondsRealtime(0.004f);
        }
        obj1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        obj1.SetActive(false);

        obj2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        obj2.SetActive(false);

        obj3.SetActive(false);

        obj4.SetActive(false);

    }
}
