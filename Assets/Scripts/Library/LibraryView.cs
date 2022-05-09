using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Library
{
    public class LibraryView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private BlueprintView _prefab;

        private readonly List<BlueprintView> _viewed = new List<BlueprintView>();
        private IImageSource _imageSource;
        private IComparer<IImmutableBlueprint> _comparer;

        public event BlueprintView.ChangedHandler OnBlueprintChanged;

        public IComparer<IImmutableBlueprint> Comparer
        {
            get => _comparer;
            set
            {
                _comparer = value;
                Sort();
            }
        }

        public void Init(IImageSource imageSource)
        {
            _imageSource = imageSource;
        }

        public bool Add(IImmutableBlueprint blueprint)
        {
            if (_viewed.Any(view => view.Showed == blueprint)) return false;

            var newView = Instantiate(_prefab, _container);
            newView.Init(_imageSource);
            newView.Showed = blueprint;
            newView.OnChanged += PassBlueprintChanged;
            _viewed.Add(newView);
            Sort();

            return true;
        }

        public bool Remove(IImmutableBlueprint blueprint)
        {
            var view = _viewed.Find(view => view.Showed == blueprint);
            if (view == null) return false;

            view.OnChanged -= PassBlueprintChanged;
            Destroy(view.gameObject);
            _viewed.Remove(view);

            return true;
        }

        public void Clear()
        {
            foreach (var view in _viewed)
            {
                view.OnChanged -= PassBlueprintChanged;
                Destroy(view);
            }
        }

        private void Sort()
        {
            _viewed.Sort((x, y) => Comparer.Compare(x.Showed, y.Showed));

            foreach (var view in _viewed)
            {
                view.transform.SetAsLastSibling();
            }
        }

        private void PassBlueprintChanged(IImmutableBlueprint blueprint, IImmutableBlueprint.Field field, object value)
        {
            OnBlueprintChanged?.Invoke(blueprint, field, value);
        }

        private void OnEnable() => _viewed.ForEach(view => view.OnChanged += PassBlueprintChanged);

        private void OnDisable() => _viewed.ForEach(view => view.OnChanged -= PassBlueprintChanged);
    }
}
