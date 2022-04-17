using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUi : MonoBehaviour
{
    [SerializeField] UiBack clearView;
    [SerializeField] GameObject[] createObjects, joinObjects;
    Vector2[] scalesCreate, scalesJoin;


    private void Start()
    {
        scalesCreate = new Vector2[createObjects.Length];
        scalesJoin = new Vector2[joinObjects.Length];

        for (int i = 0; i < createObjects.Length; i++)
        {
            scalesCreate[i] = createObjects[i].GetComponent<RectTransform>().localScale/2;
        }
        for (int i = 0; i < joinObjects.Length; i++)
        {
            scalesJoin[i] = joinObjects[i].GetComponent<RectTransform>().localScale;
        }
    }

    public void ClearView(bool creating)
    {
        clearView.Back(false);

        if (creating)
        {
            foreach (var _obj in createObjects)
            {
                _obj.GetComponent<RectTransform>().localScale = Vector2.zero;
                _obj.SetActive(true);
            }
            StartCoroutine("CoroutineClearView", creating);
        }

        else
        {
            foreach (var _obj in joinObjects)
            {
                _obj.GetComponent<RectTransform>().localScale = Vector2.zero;
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
            yield return new WaitForSecondsRealtime(0.015f);
        }
    }
}
