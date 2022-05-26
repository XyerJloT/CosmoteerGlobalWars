using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Assets.Scripts.Map
{
    public class StarView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private int _incom = 0;
        [SerializeField] private string _name = "Default";
        [SerializeField] private Text _description;
        [SerializeField] private Image _stroke;
        [SerializeField] private Behaviour _selection;

        private Star _star;

        public UnityEvent<StarView> OnClick;

        public Star Showed => _star;

        public void Init(Star star)
        {
            _star = star;
            _star.OnOwnerChanged += HandleCapture;
            _stroke.color = _star.Owner.Color;

            gameObject.transform.position = _star.Position;
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
            OnClick?.Invoke(this);
        }

        private void Start()
        {
            _selection.enabled = false;
            _description.text = MinimalDescription();
        }

        private string MinimalDescription()
        {
            return _incom + " \u20A1";
        }

        private string Description()
        {
            return $"{_name} - {_incom} \u20A1";
        }

        private void HandleCapture(Team oldOwner, Team invader)
        {
            _stroke.color = invader.Color;
        }
    }
}
