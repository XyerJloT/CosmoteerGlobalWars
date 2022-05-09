using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Library
{
    public class BlueprintView : MonoBehaviour
    {
        [SerializeField] private RawImage _icon;
        [SerializeField] private Text _name;
        [SerializeField] private NumberField _cost;

        private IImageSource _imageSource;
        private IImmutableBlueprint _showed;

        public delegate void ChangedHandler(IImmutableBlueprint blueprint, IImmutableBlueprint.Field field, object value);

        public event ChangedHandler OnChanged;

        public IImmutableBlueprint Showed
        {
            get => _showed;
            set
            {
                _icon.texture = _imageSource.Load(value.IconPath);
                _name.text = value.Name;
                _cost.Number = value.Cost;
                _showed = value;
            }
        }

        public void Init(IImageSource imageSource)
        {
            _imageSource = imageSource;
        }

        private void HandleCostChange(int cost) => OnChanged?.Invoke(Showed, IImmutableBlueprint.Field.Cost, cost);

        private void OnEnable() => _cost.OnEndEdit.AddListener(HandleCostChange);
        
        private void OnDisable() => _cost.OnEndEdit.RemoveListener(HandleCostChange);
    }
}
