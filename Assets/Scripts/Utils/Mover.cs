using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Вешается на объект на который надо кликнуть, что бы перетянуть объект
public class Mover : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _whoWillBeMoved;

    public void OnDrag(PointerEventData eventData)
    {
        _whoWillBeMoved.anchoredPosition += eventData.delta;
    }
}
