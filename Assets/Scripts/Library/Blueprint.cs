namespace Assets.Scripts.Library
{
    public class Blueprint : IImmutableBlueprint
    {
        public string Name { get; set; }

        public string IconPath { get; set; }

        public int Cost { get; set; }

        public Ship.RankType Rank { get; set; }

        public Blueprint() { }

        public Blueprint(IImmutableBlueprint other)
        {
            Name = other.Name;
            IconPath = other.IconPath;
            Cost = other.Cost;
            Rank = other.Rank;
        }
    }
}
