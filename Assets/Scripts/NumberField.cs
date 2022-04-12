using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NumberField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private int _number;
    [SerializeField] private bool _short;
    [SerializeField] private InputField _input;
    
    public UnityEvent<int> OnChanged;

    public int Number
    {
        get => _number;
        set
        {
            // Если кто-то выбрал поле, то снимаем выделение
            if (_isSelected)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            _input.contentType = InputField.ContentType.Standard;
            _input.text = Formated(value);
            _number = value;

            OnChanged?.Invoke(value);
        }
    }

    public bool IsShort
    {
        get => _short;
        set
        {
            _short = value;
            Number = _number;
        }
    }

    private bool _isSelected;

    public void OnSelect(BaseEventData eventData)
    {
        _input.contentType = InputField.ContentType.IntegerNumber;
        _input.text = _number.ToString();
        _isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        Number = int.Parse(_input.text);
    }

    private void Start()
    {
        // Инициализация значения из инспектора
        Number = _number;
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            // Обновление значения из инспектора
            Number = _number;
        }
    }

    private string Formated(int number)
    {
        return _short ? ShortFormat(number) : number.ToString(); 
    }

    private string ShortFormat(int number)
    {
        return new MetricPrefix(number).ToString();
    }
}
