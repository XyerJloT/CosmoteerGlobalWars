public class Ship
{
    public enum RankType
    {
        Corvette,
        Frigate,
        Destroyer,
        Cruiser,
        Battleship,
        Titan
    }

    public readonly string Name;
    public readonly RankType Rank;

    public Ship(string name, RankType rank)
    {
        Name = name;
        Rank = rank;
    }
}
