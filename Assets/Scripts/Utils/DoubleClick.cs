using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Задержка в секундах")]
    [SerializeField] private float _maxDurationBetweenClicks = 0.25f;

    public UnityEvent OnDoubleClick;

    private float _lastClickTime = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        var time = Time.time;

        if (time - _lastClickTime <= _maxDurationBetweenClicks)
        {
            OnDoubleClick?.Invoke();
        }

        _lastClickTime = time;
    }
}
