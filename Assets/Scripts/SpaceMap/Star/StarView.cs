using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StarView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int _incom = 0;
    [SerializeField] private string _name = "Default";
    [SerializeField] private Text _description;
    [SerializeField] private Image _stroke;
    [SerializeField] private Behaviour _selection;

    public StarModel Model { get; private set; }

    private void Start()
    {
        _description.text = MinimalDescription();

        _selection.enabled = false;

        Model = new StarModel(_name, _incom);
        Model.OnCaptured += invader => _stroke.color = invader.Color; // Отписываться не обязательно т.к. Model всегда привязан к StarView
        Model.Capture(Team.Neutral);
    }

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
        //TODO: Open build menu
    }

    private string MinimalDescription()
    {
        return $"{_incom}$";
    }

    private string Description()
    {
        return $"{_name} - {_incom}$";
    }
}
