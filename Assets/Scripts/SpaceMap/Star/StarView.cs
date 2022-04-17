using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StarView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int _incom = 0;
    [SerializeField] private string _name = "Default";
    [SerializeField] private Text _description;
    [SerializeField] private Image _stroke;
    [SerializeField] private Behaviour _selection;

    public Star Model { get; private set; }

    public UnityEvent<Star> OnClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selection.enabled = true;
        _description.text = Description();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selection.enabled = false;
        _description.text = MinimalDescription();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(Model);
    }

    private void Start()
    {
        _selection.enabled = false;
        _description.text = MinimalDescription();

        Model = new Star(_name, _incom);
        Model.OnCaptured += HandleCapture;
        Model.Capture(Team.Neutral);
    }

    private string MinimalDescription()
    {
        return _incom + " \u20A1";
    }

    private string Description()
    {
        return $"{_name} - {_incom} \u20A1";
    }

    private void HandleCapture(Team invader)
    {
        _stroke.color = invader.Color;
    }
}
