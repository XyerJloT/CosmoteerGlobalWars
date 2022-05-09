using System;
using System.Collections.Specialized;

namespace Assets.Scripts.Library
{
    public class LibraryPresenter : IDisposable
    {
        private Library _model;
        private LibraryView _view;

        public LibraryPresenter(Library model, LibraryView view)
        {
            _model = model;
            _view = view;

            _view.Clear();
            foreach (var blueprint in _model)
            {
                _view.Add(blueprint);
            }

            _model.CollectionChanged += HandleModelChanged;
            _view.OnBlueprintChanged += HandleBlueprintChanged;
        }

        public void Dispose()
        {
            _model.CollectionChanged -= HandleModelChanged;
            _view.OnBlueprintChanged -= HandleBlueprintChanged;
        }

        private void HandleBlueprintChanged(IImmutableBlueprint blueprint, IImmutableBlueprint.Field field, object value)
        {
            var newBlueprint = new Blueprint(blueprint);

            switch (field)
            {
                case IImmutableBlueprint.Field.Name:
                    newBlueprint.Name = (string)value;
                    break;
                case IImmutableBlueprint.Field.IconPath:
                    newBlueprint.IconPath = (string)value;
                    break;
                case IImmutableBlueprint.Field.Rank:
                    newBlueprint.Rank = (Ship.RankType)value;
                    break;
                case IImmutableBlueprint.Field.Cost:
                    newBlueprint.Cost = (int)value;
                    break;
                default: throw new ArgumentException($"Не обработаное изменение поля '{field}'");
            }

            _model.Remove(blueprint);
            _model.Add(newBlueprint);
        }

        private void HandleModelChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        _view.Add((IImmutableBlueprint)item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        _view.Remove((IImmutableBlueprint)item);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _view.Clear();
                    break;
                default: throw new ArgumentException($"Не обработаное событие '{e.Action}' изменения модели");
            }
        }
    }
}
