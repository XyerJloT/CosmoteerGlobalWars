using Assets.Scripts.Map;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Serialization.Map
{
    public class MapAdapter : ISerializeAdapter<SpaceMap>, IDeserializeAdapter<SpaceMap>
    {
        public List<TeamAdapter> teams;
        public List<StarAdapter> stars;
        public List<TunnelAdapter> tunnels;

        public SpaceMap GetOriginal()
        {
            var teamMap = teams
                .Select(t => t.GetOriginal())
                .ToDictionary(t => t.Id);

            var starMap = stars
                .Select(s => s.GetOriginal(teamMap))
                .ToDictionary(s => s.Name);

            var tunnels = this.tunnels.Select(t => t.GetOriginal(starMap));

            return new SpaceMap(tunnels);
        }

        public void SetOriginal(SpaceMap value)
        {
            teams = value.Stars
                .Select(s => s.Owner)
                .Distinct()
                .Select(AdapterFactory<Team, TeamAdapter>.Create)
                .ToList();

            stars = value.Stars
                .Select(AdapterFactory<Star, StarAdapter>.Create)
                .ToList();

            tunnels = value.AllTunnels()
                .Select(AdapterFactory<Tunnel, TunnelAdapter>.Create)
                .ToList();
        }  
    }
}
