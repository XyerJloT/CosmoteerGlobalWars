using UnityEngine;
using UnityEngine.UI;
public class RotateImage : MonoBehaviour
{
    Sprite _startSprite;
    [SerializeField] Sprite _newSprite;
    bool _alreadyClicked;

    private void Start()
    {
        _startSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void ChangeImage()
    {
        if (!_alreadyClicked)
        {
            gameObject.GetComponent<Image>().sprite = _newSprite;
            _alreadyClicked = true;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = _startSprite;
            _alreadyClicked = false;
        }
    }
}
