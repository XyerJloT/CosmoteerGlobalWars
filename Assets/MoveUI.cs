using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    [SerializeField] List<RectTransform> rect;
    List <Vector2> startPos;
    [Header("Целевая позиция")][SerializeField] Vector2 _newPos;
    [Header("Скорость шага (0.01)")] [SerializeField] float _speed = 0.01f;
    [Header("К-во шагов (10)")] [SerializeField] int _stepCount = 10;

    bool _alreadyClickedLR, _alreadyClickedUD;

    private void Start()
    {
        startPos = new List<Vector2>(rect.Count);
        for (int i = 0; i < rect.Count; i++)
        {
            startPos.Add(rect[i].localPosition);
        }
    }

    public void MoveLeftRight()
    {
        List<Vector2> diff = new List<Vector2>(startPos.Count);
        for (int i = 0; i < startPos.Count; i++)
        {
            diff.Add(new Vector2(Mathf.Abs(Mathf.Abs(startPos[i].x) - Mathf.Abs(_newPos.x)), 0));
        }

        if (!_alreadyClickedLR)
        {
            StartCoroutine("Left", diff);
            _alreadyClickedLR = true;
        }
        else
        {
            StartCoroutine("Right", diff);
            _alreadyClickedLR = false;
        }
    }

    public void MoveRightLeft()
    {
        List<Vector2> diff = new List<Vector2>(rect.Count);
        for (int i = 0; i < rect.Count; i++)
        {
            diff.Add(new Vector2(Mathf.Abs(Mathf.Abs(startPos[i].x) - Mathf.Abs(_newPos.x)), 0));
        }

        if (!_alreadyClickedLR)
        {
            StartCoroutine("Right", diff);
            _alreadyClickedLR = true;
        }
        else
        {
            StartCoroutine("Left", diff);
            _alreadyClickedLR = false;
        }
    }
    
    public void MoveUpDown()
    {
        List<Vector2> diff = new List<Vector2>(startPos.Count);
        for (int i = 0; i < startPos.Count; i++)
        {
            diff.Add(new Vector2(Mathf.Abs(Mathf.Abs(startPos[i].x) - Mathf.Abs(_newPos.x)), 0));
        }

        if (!_alreadyClickedUD)
        {
            StartCoroutine("Up", diff);
            _alreadyClickedUD = true;
        }
        else
        {
            StartCoroutine("Down", diff);
            _alreadyClickedUD = false;
        }
    }

    public void MoveDownUp()
    {
        List<Vector2> diff = new List<Vector2>(startPos.Count);
        for (int i = 0; i < startPos.Count; i++)
        {
            diff.Add(new Vector2(Mathf.Abs(Mathf.Abs(startPos[i].x) - Mathf.Abs(_newPos.x)), 0));
        }

        if (!_alreadyClickedUD)
        {
            StartCoroutine("Down", diff);
            _alreadyClickedUD = true;
        }
        else
        {
            StartCoroutine("Up", diff);
            _alreadyClickedUD = false;
        }
    }

    IEnumerator Right(List<Vector2> diff)
    {
        for (int i = 0; i < _stepCount; i++)
        {
            for (int r = 0; r < rect.Count; r++)
            {
                rect[r].localPosition += new Vector3(diff[r].x / _stepCount, 0, 0);
            }
            yield return new WaitForSecondsRealtime(_speed);
        }
    }

    IEnumerator Left(List<Vector2> diff)
    {
        for (int i = 0; i < _stepCount; i++)
        {
            for (int r = 0; r < rect.Count; r++)
            {
                rect[r].localPosition -= new Vector3(diff[r].x / _stepCount, 0, 0);
            }
            yield return new WaitForSecondsRealtime(_speed);
        }
    }
    
    IEnumerator Up(List<Vector2> diff)
    {
        for (int i = 0; i < _stepCount; i++)
        {
            for (int r = 0; r < rect.Count; r++)
            {
                rect[r].localPosition += new Vector3(0, diff[r].y / _stepCount, 0);
            }
            yield return new WaitForSecondsRealtime(_speed);
        }
    }

    IEnumerator Down(List<Vector2> diff)
    {
        for (int i = 0; i < _stepCount; i++)
        {
            for (int r = 0; r < rect.Count; r++)
            {
                rect[r].localPosition += new Vector3(0, diff[r].y / _stepCount, 0);
            }
            yield return new WaitForSecondsRealtime(_speed);
        }
    }
}
