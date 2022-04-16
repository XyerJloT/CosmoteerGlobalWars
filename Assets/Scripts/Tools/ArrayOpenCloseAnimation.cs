using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayOpenCloseAnimation : MonoBehaviour
{
    [Header("Объекты для открытия/закрытия")] [SerializeField] List<RectTransform> objectsToResize;
    [Header("Скорость шага (0,01)")] [SerializeField] float _speed = 0.01f;
    [Header("К-во шагов (10)")] [SerializeField] float _stepsCount = 10;
    [Header("Деактивировать объект при уменьшении")] [SerializeField] bool _deactivate;
    List<Vector2> scales;
    [Space]
    [SerializeField] bool _isOpened;
    bool _process;

    private void Start()
    {
        scales = new List<Vector2>(objectsToResize.Count);
        for (int i = 0; i < objectsToResize.Count; i++)
        {
            scales.Add(objectsToResize[i].sizeDelta);
            objectsToResize[i].sizeDelta = Vector2.zero;
            if (_deactivate) objectsToResize[i].gameObject.SetActive(false);
        }
    }

    public void OpenClose()
    {
        if (!_isOpened && !_process) StartCoroutine("OpenAnimation");
        else if(_isOpened && !_process) StartCoroutine("CloseAnimation");
    }
    IEnumerator OpenAnimation()
    {
        if (_deactivate)
        {
            foreach (var item in objectsToResize)
            {
                item.gameObject.SetActive(true);
            }
        }
        _process = true;
        for (int i = 0; i < _stepsCount; i++)
        {
            for (int o = 0; o < objectsToResize.Count; o++)
            {
                objectsToResize[o].sizeDelta += new Vector2(scales[o].x / _stepsCount, scales[o].y / _stepsCount);
            }
            yield return new WaitForSecondsRealtime(_speed);
        }
        _isOpened = true;
        _process = false;
    }

    IEnumerator CloseAnimation()
    {
        _process = true;
        for (int i = 0; i < _stepsCount; i++)
        {
            for (int o = 0; o < objectsToResize.Count; o++)
            {
                objectsToResize[o].sizeDelta -= new Vector2(scales[o].x / _stepsCount, scales[o].y / _stepsCount);
            }
            yield return new WaitForSecondsRealtime(_speed);
        }
        _isOpened = false;
        _process = false;

        if (_deactivate)
        {
            foreach (var item in objectsToResize)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
