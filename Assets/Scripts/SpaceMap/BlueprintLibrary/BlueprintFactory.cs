public class BlueprintFactory
{
    private IShipRankMatcher _matcher;

    public BlueprintFactory(IShipRankMatcher matcher)
    {
        _matcher = matcher;
    }

    public Blueprint Create(string name, int cost, string iconPath)
    {
        return new Blueprint(name, _matcher.MatchRank(cost), cost, iconPath);
    }
}
