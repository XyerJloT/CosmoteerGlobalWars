using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Assets.Scripts.Library
{
    public class Library : IEnumerable<IImmutableBlueprint>, INotifyCollectionChanged
    {
        private readonly HashSet<IImmutableBlueprint> _container = new HashSet<IImmutableBlueprint>();
        private readonly IRankMatcher _rankMatcher;

        public Library(IRankMatcher rankMatcher)
        {
            _rankMatcher = rankMatcher;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool Contains(IImmutableBlueprint blueprint) => _container.Contains(blueprint);

        public bool ContainsByName(string name) => _container.Any(b => b.Name.Equals(name));

        public bool Add(IImmutableBlueprint blueprint)
        {
            var validated = Validate(blueprint);
            if (!_container.Add(validated)) return false;

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, validated));
            return true;
        }

        public bool Remove(IImmutableBlueprint blueprint)
        {
            if (!_container.Remove(blueprint)) return false;

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, blueprint));
            return true;
        }

        public void Clear()
        {
            _container.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerator<IImmutableBlueprint> GetEnumerator() => _container.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IImmutableBlueprint Validate(IImmutableBlueprint blueprint)
        {
            return new Blueprint(blueprint)
            {
                Rank = _rankMatcher.MatchByCost(blueprint.Cost)
            };
        }
    }
}
