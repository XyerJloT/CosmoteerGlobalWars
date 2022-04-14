using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPosition : MonoBehaviour
{
    [SerializeField] RectTransform _position;
    public Vector3 _offset;
    RectTransform _rect;
    private void Start()
    {
        _rect = gameObject.GetComponent<RectTransform>();
      
    }
    private void Update()
    {
        _rect.localPosition = new Vector3(_position.localPosition.x - _offset.x, _position.localPosition.y - _offset.y, 0);
    }
}
