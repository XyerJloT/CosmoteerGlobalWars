using System;

[Serializable]
public class Blueprint
{
    public readonly string Name;
    public readonly int Cost;
    public readonly string IconPath;

    [NonSerialized] public readonly Ship.RankType Type;

    public Blueprint(string name, Ship.RankType type, int cost, string iconPath)
    {
        Name = name;
        Type = type;
        Cost = cost;
        IconPath = iconPath;
    }
}

