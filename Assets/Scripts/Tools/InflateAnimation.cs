using System;
using System.Collections;
using UnityEngine;

public class InflateAnimation : MonoBehaviour
{
    // Если нужно анимировать несколько объектов, сделай им общего объекта-родителя и анимируй его
    [Tooltip("Объект который будет анимирован")]
    [SerializeField] private Transform _animatedTransform;
    
    [Tooltip("Длительность анимации в секундах")]
    [SerializeField, Min(0)] private float _animationDuration = 0.1f;
    
    [Tooltip("Количество кадров анимации")]
    [SerializeField, Min(1)] private int _frames = 10;

    [Tooltip("Деактивировать объект пока закрыт")]
    [SerializeField] private bool _willBeDeactivated = false;
    
    // Приватные сеттер, что бы случайно не изменили значения флага из другого скрипта
    public bool IsOpened { get; private set; }

    private Vector3 _baseScale;

    public void Open()
    {
        if (IsOpened) return;

        StopAllCoroutines();
        StartCoroutine(OpenAnimation());
    }

    public void Close()
    {
        if (!IsOpened) return;
        
        StopAllCoroutines();
        StartCoroutine(CloseAnimation());
    }
    
    private void Start()
    {
        _baseScale = _animatedTransform.localScale;
        _animatedTransform.localScale = Vector3.zero;

        if (_willBeDeactivated) _animatedTransform.gameObject.SetActive(false);
    }

    private IEnumerator OpenAnimation()
    {
        if (_willBeDeactivated) _animatedTransform.gameObject.SetActive(true);
        yield return ScaleAnimation(i => (i + 1) / _frames);

        IsOpened = true;
    }

    private IEnumerator CloseAnimation()
    {
        yield return ScaleAnimation(i => 1 - (i + 1) / _frames);
        if (_willBeDeactivated) _animatedTransform.gameObject.SetActive(false);

        IsOpened = false;
    }

    private IEnumerator ScaleAnimation(Func<float, float> partFormula)
    {
        float frameDuration = _animationDuration / _frames;

        for (int i = 0; i < _frames; i++)
        {
            float part = partFormula(i);

            _animatedTransform.localScale = part * _baseScale;

            yield return new WaitForSecondsRealtime(frameDuration);
        }
    }
}
