using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UiMove : MonoBehaviour
{
    private RectTransform _rect;
    private Vector2 _startPos;
    [SerializeField] Vector2 _newPos;
    bool _alredyActivated = false;

    private void Start()
    {
        _rect = gameObject.GetComponent<RectTransform>();
        _startPos = _rect.localPosition;
    }
    public void OnClickRightAndLeft()
    {
        print("ckic");
        StartCoroutine("RectTransform");
    }

    public IEnumerator RectTransform()
    {
        if (!_alredyActivated)
        {
            Vector2 _diff = new Vector2(Mathf.Abs(_startPos.x) - Mathf.Abs(_newPos.x), 0);

            for (int i = 0; i < 10; i++)
            {
                _rect.localPosition += new Vector3(_diff.x / 10, _diff.y / 10, 0);
                yield return new WaitForSecondsRealtime(0.01f);
            }
            _alredyActivated = true;
        }

        else
        {
            Vector2 _diff = new Vector2(Mathf.Abs(_startPos.x) - Mathf.Abs(_newPos.x), 0);

            for (int i = 0; i < 10; i++)
            {
                _rect.localPosition -= new Vector3(_diff.x / 10, _diff.y / 10, 0);
                yield return new WaitForSecondsRealtime(0.01f);
            }
            _alredyActivated = false;
        }
    }
}
