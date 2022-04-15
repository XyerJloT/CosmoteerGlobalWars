public class Blueprint
{
    public readonly string Name;
    public readonly Ship.RankType Type;
    public readonly int Cost;

    public Blueprint(string name, Ship.RankType type, int cost)
    {
        Name = name;
        Type = type;
        Cost = cost;
    }

    public Ship Create()
    {
        return new Ship(Name, Type);
    }
}

