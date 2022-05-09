using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �������� �� ������ �� ������� ���� ��������, ��� �� ���������� ������
public class Mover : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _whoWillBeMoved;

    public void OnDrag(PointerEventData eventData)
    {
        _whoWillBeMoved.anchoredPosition += eventData.delta;
    }
}
