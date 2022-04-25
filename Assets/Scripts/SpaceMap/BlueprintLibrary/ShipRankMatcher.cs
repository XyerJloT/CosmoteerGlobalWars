public class ShipRankMatcher : IShipRankMatcher
{
    public Ship.RankType MatchRank(int cost)
    {
        if (cost <= 75_000) return Ship.RankType.Corvette;
        if (cost <= 150_000) return Ship.RankType.Frigate;
        if (cost <= 250_000) return Ship.RankType.Destroyer;
        if (cost <= 500_000) return Ship.RankType.Cruiser;
        if (cost <= 1_000_000) return Ship.RankType.Battleship;
        return Ship.RankType.Titan;
    }
}
