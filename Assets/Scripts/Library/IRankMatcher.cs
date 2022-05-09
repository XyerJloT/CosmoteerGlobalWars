namespace Assets.Scripts.Library
{
    public interface IRankMatcher
    {
        public Ship.RankType MatchByCost(int cost);
    }
}
