using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Map
{
    public class SpaceMap
    {
        public readonly IEnumerable<Star> Stars;

        private readonly Dictionary<Star, List<Tunnel>> _tunnelMap = new Dictionary<Star, List<Tunnel>>();

        public SpaceMap(IEnumerable<Tunnel> tunnels)
        {
            Stars = tunnels.SelectMany(t => new Star[] { t.From, t.To }).Distinct().ToList();

            foreach (var star in Stars)
            {
                _tunnelMap.Add(star, new List<Tunnel>());
            }

            foreach (var tunnel in tunnels)
            {
                _tunnelMap[tunnel.From].Add(tunnel);
            }
        }

        public IEnumerable<Tunnel> GetTunnelsOnStar(Star star) => _tunnelMap[star];

        public IEnumerable<Tunnel> AllTunnels() => _tunnelMap.Values.SelectMany(list => list);
    }
}
