using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NumberField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private int _number;
    [SerializeField] private bool _short;

    [SerializeField] private InputField _input;

    private bool _isSelected;

    public int Number
    {
        get => _number;
        set => UpdateField(value);
    }

    public bool IsShort
    {
        get => _short;
        set
        {
            _short = value;
            UpdateField(_number);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _input.contentType = InputField.ContentType.IntegerNumber;
        _input.text = _number.ToString();
        _isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        UpdateField(int.Parse(_input.text));
    }

    private void Start()
    {
        UpdateField(_number);
    }

    private void Update()
    {
        // Что бы в редакторе обновлялись циферки
        if (!Application.isPlaying)
        {
            UpdateField(_number);
        }
    }

    private void UpdateField(int number)
    {
        // Если кто-то выбрал поле, то снимаем выделение
        if (_isSelected)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        _input.contentType = InputField.ContentType.Standard;
        _input.text = Formated(number);
        _number = number;
    }

    private string Formated(int number)
    {
        return _short ? ShortFormat(number) : number.ToString(); 
    }

    private string ShortFormat(int number)
    {
        //TODO: Ультра мега форматирование с приставками K, M и G
        return "short " + number;
    }
}
