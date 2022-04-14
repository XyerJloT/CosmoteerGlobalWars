using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStart : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToActivate;
    Vector2[] scales;
    Vector2 _startScale;
    private void Start()
    {
        scales = new Vector2[objectsToActivate.Length];
        _startScale = gameObject.GetComponent<RectTransform>().sizeDelta;
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            scales[i] = objectsToActivate[i].GetComponent<RectTransform>().sizeDelta;
        }
    }

    public void ScaleUI()
    {
        foreach (var _obj in objectsToActivate)
        {
            _obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            _obj.SetActive(true);
        }
        StartCoroutine("ScaleCoroutine") ;
    }

    private IEnumerator ScaleCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            FillArray fill = new FillArray();
            fill.FillRectSizePlus(objectsToActivate, scales);
            gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(_startScale.x / 10, _startScale.y / 10);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        GameObject.Find("MenuField").GetComponent<UiMenu>().MenuStart();
        gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().sizeDelta = _startScale;
    }
}
