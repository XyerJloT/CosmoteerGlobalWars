using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Star
    {
        public readonly string Name;
        public readonly int Incom;
        public readonly Vector2 Position;

        private Team _owner;

        public Star(string name, int incom, Vector2 position)
        {
            Name = name;
            Incom = incom;
            Position = position;
        }

        public delegate void OwnerChangedHandler(Team oldOwner, Team invader);

        public event OwnerChangedHandler OnOwnerChanged;

        public Team Owner => _owner;

        public void TakeOver(Team invader)
        {
            var oldOwner = _owner;
            _owner = invader;

            OnOwnerChanged?.Invoke(oldOwner, invader);
        }
    }
}
