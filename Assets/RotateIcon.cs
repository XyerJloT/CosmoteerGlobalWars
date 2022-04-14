using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateIcon : MonoBehaviour
{
    private Image _image;
    private Sprite _startSprite;
    public Sprite _reverseObjectVersion;
    bool _alreadyClicked = false;
    private void Start()
    {
        _image = gameObject.GetComponent<Image>();
        _startSprite = _image.sprite;
    }
    public void RotateObjectIcon()
    {
        if (!_alreadyClicked)
        {
            _image.sprite = _reverseObjectVersion;
            _alreadyClicked = true;
        }
        else
        {
            _image.sprite = _startSprite;
            _alreadyClicked = false;
        }
    }
}
