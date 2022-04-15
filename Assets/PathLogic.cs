using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLogic : MonoBehaviour
{
    public GameObject _star1, _star2;
    float distance;
    Vector2 _centralPos;
    float _angle;

    private void Update()
    {
      //  if (_star1 == null && _star2 == null) Destroy(gameObject);

        distance = Vector2.Distance(_star1.transform.position, _star2.transform.position);
        _centralPos = Vector2.Lerp(_star1.transform.position, _star2.transform.position, 0.5f);

        Vector3 difference = _star1.transform.position - _star2.transform.position;
        _angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

        gameObject.transform.position = _centralPos;
        gameObject.transform.rotation = Quaternion.Euler(0f, 180f, _angle);
        gameObject.transform.localScale = new Vector2(transform.localScale.x, distance);
    }
}
