namespace Assets.Scripts.Library
{
    public interface IImmutableBlueprint
    {
        public string Name { get; }
        public string IconPath { get; }
        public int Cost { get; }
        public Ship.RankType Rank { get; }

        public enum Field { Name, IconPath, Cost, Rank }
    }
}
