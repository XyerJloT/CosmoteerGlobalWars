using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiMenu : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToResize;
    Vector2[] scales;
    private void Start()
    {
        scales = new Vector2[objectsToResize.Length];
        for (int i = 0; i < objectsToResize.Length; i++)
        {
            scales[i] = objectsToResize[i].GetComponent<RectTransform>().localScale;
        }
    }
    public void MenuStart()
    {
        foreach (var _obj in objectsToResize)
        {
            _obj.GetComponent<RectTransform>().localScale = Vector2.zero;
        }
        StartCoroutine("CoroutineStart");
    }

    IEnumerator CoroutineStart()
    {
        foreach (var _obj in objectsToResize)
        {
            _obj.SetActive(true);
        }

        for (int i = 0; i < 10; i++)
        {
            FillArray fill = new FillArray();
            fill.FillRectSizePlus(objectsToResize, scales);
            yield return new WaitForSecondsRealtime(0.011f);
        }
    }

    public void MenuClose()
    {
        StartCoroutine("CoroutineClose");
    }

    IEnumerator CoroutineClose()
    {
        for (int i = 0; i < 10; i++)
        {
            FillArray fill = new FillArray();
            fill.FillRectSizeMinus(objectsToResize, scales);
            yield return new WaitForSecondsRealtime(0.004f);
        }

        foreach (var _obj in objectsToResize)
        {
            _obj.SetActive(false);
        }
    }
}
