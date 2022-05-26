using Assets.Scripts.Map;
using System.Collections.Generic;

namespace Assets.Scripts.Serialization.Map
{
    using StarMap = Dictionary<string, Star>;

    public class TunnelAdapter : ISerializeAdapter<Tunnel>, IDeserializeAdapter<Tunnel, StarMap>
    {
        public string from, to;

        public Tunnel GetOriginal(StarMap context)
        {
            var starFrom = context[from];
            var starTo = context[to];

            return new Tunnel(starFrom, starTo);
        }

        public void SetOriginal(Tunnel value)
        {
            from = value.From.Name;
            to = value.To.Name;
        }
    }
}
