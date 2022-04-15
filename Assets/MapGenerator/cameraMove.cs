using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    Vector2 _startPos;

    Vector2 _currentPos;

    Vector2 _distance;

    Vector2 _newPos;

    Camera _cam;

    public float _camSpeed;

    private void Update()
    {
        Zoom();
        _cam = Camera.main;
        MouseMove();
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && _cam.orthographicSize >= 1)
        {
            StartCoroutine("MouseScrollMove", true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && _cam.orthographicSize < 5)
        {
            StartCoroutine("MouseScrollMove", false);
        }
    }

    IEnumerator MouseScrollMove(bool plus)
    {
        if (plus)
        {
            for (int i = 0; i < 10; i++)
            {
                _cam.orthographicSize -= 0.02f;
                yield return new WaitForSecondsRealtime(0.0005f);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                _cam.orthographicSize += 0.02f;
                yield return new WaitForSecondsRealtime(0.0005f);
            }
        }
    }

    private void MouseMove()
    {

       /* if (Input.GetMouseButtonDown(2)) //запоминаем позицию до нажатия средней кнопки мыши
        {
            _startPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }*/

        if (Input.GetMouseButton(2)) //запоминаем позицию после нажатия средней кнопки мыши
        {
             _currentPos = _cam.ScreenToWorldPoint(Input.mousePosition);

           /* Vector2 difference = new Vector2(_startPos.x - _currentPos.x, _startPos.y - _currentPos.x);

            _distance = new Vector3(Mathf.Abs(difference.x), Mathf.Abs(difference.y)); //расстояние, на которое надо передвинуть

            if(_currentPos.x > _startPos.x) _newPos = new Vector3(transform.position.x - _distance.x, transform.position.y); //двигаем налево, если новая позиция правее изначальной
            else if(_currentPos.x < _startPos.x) _newPos = new Vector3(transform.position.x + _distance.x, transform.position.y); //направо

            if (_currentPos.y > _startPos.y) _newPos = new Vector3(transform.position.x, transform.position.y - _distance.y); //вниз
            else if (_currentPos.y < _startPos.y) _newPos = new Vector3(transform.position.x, transform.position.y + _distance.y); //вверх*/

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _currentPos, _camSpeed);
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    
        }
    }
}
