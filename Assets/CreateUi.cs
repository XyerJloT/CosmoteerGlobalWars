using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUi : MonoBehaviour
{
    [SerializeField] UiBack clearView;
    [SerializeField] GameObject[] createObjects, joinObjects;
    Vector2[] scalesCreate, scalesJoin;
    [SerializeField] GameObject _back;
    Vector2 _backScale;


    private void Start()
    {
        scalesCreate = new Vector2[createObjects.Length];
        scalesJoin = new Vector2[joinObjects.Length];
        _backScale = _back.GetComponent<RectTransform>().sizeDelta;

        for (int i = 0; i < createObjects.Length; i++)
        {
            scalesCreate[i] = createObjects[i].GetComponent<RectTransform>().sizeDelta;
        }
        for (int i = 0; i < joinObjects.Length; i++)
        {
            scalesJoin[i] = joinObjects[i].GetComponent<RectTransform>().sizeDelta;
        }
    }

    public void ClearView(bool creating)
    {
        clearView.Back(false);
        _back.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        _back.SetActive(true);

        if (creating)
        {
            foreach (var _obj in createObjects)
            {
                _obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                _obj.SetActive(true);
            }
        StartCoroutine("CoroutineClearView", creating);
        }

        else
        {
            foreach (var _obj in joinObjects)
            {
                _obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                _obj.SetActive(true);
            }

        }
        StartCoroutine("CoroutineClearView", creating);
    }

    IEnumerator CoroutineClearView(bool creating)
    {
        for (int i = 0; i < 10; i++)
        {
            if (creating)
            {
                FillArray fill = new FillArray();
                fill.FillRectSizePlus(createObjects, scalesCreate);
            }
            
            else
            {
                FillArray fill = new FillArray();
                fill.FillRectSizePlus(joinObjects, scalesJoin);
            }
            _back.GetComponent<RectTransform>().sizeDelta += new Vector2(_backScale.x / 10, _backScale.y / 10);
            yield return new WaitForSecondsRealtime(0.004f);
        }
    }
}
