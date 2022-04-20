using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class InflateAnimation : MonoBehaviour
{
    [Tooltip("Длительность анимации в секундах")]
    [SerializeField, Min(0)] private float _animationDuration = 0.1f;
    
    [Tooltip("Количество кадров анимации")]
    [SerializeField, Min(1)] private int _frames = 10;

    // Если нужно анимировать несколько объектов, сделай им общего объекта-родителя и анимируй его
    private Transform _animatedTransform;
    private Vector3 _baseScale;

    public IEnumerator OpenCoroutine()
    {
        yield return ScaleAnimation(i => (i + 1) / _frames);
    }

    public IEnumerator CloseCoroutine()
    {
        yield return ScaleAnimation(i => 1 - (i + 1) / _frames);
    }

    private void Start()
    {
        _animatedTransform = GetComponent<Transform>();

        _baseScale = _animatedTransform.localScale;
        _animatedTransform.localScale = Vector3.zero;
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
