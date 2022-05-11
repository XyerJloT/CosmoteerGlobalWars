using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NumberField : MonoBehaviour, ISelectHandler
{
    [SerializeField] private int _number;
    [SerializeField] private bool _short;
    [SerializeField] private InputField _input;
    
    public UnityEvent<int> OnChanged;
    public UnityEvent<int> OnEndEdit;

    public int Number
    {
        get => _number;
        set
        {
            // Если вызывается из другого скрипта, то надо снять выделение
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

    private void OnEnable()
    {
        _input.onEndEdit.AddListener(EndEditHandler);
    }

    private void OnDisable()
    {
        _input.onEndEdit.RemoveListener(EndEditHandler);
    }

    private void Start()
    {
        // Инициализация значения из инспектора
        Number = _number;        
    }

    private void Update()
    {
        if (!Application.isPlaying) Number = _number;
    }

    private void EndEditHandler(string value)
    {
        _isSelected = false;
        Number = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
        
        OnEndEdit?.Invoke(Number);
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
