using Assets.Scripts.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Serialization.Map
{
    using TeamMap = Dictionary<Team.TeamId, Team>;

    public class StarAdapter : ISerializeAdapter<Star>, IDeserializeAdapter<Star, TeamMap>
    {
        public string name;
        public int incom;
        public Team.TeamId owner_id;
        public Vector2 position;

        public Star GetOriginal(TeamMap context)
        {
            var team = context[owner_id];
            var star = new Star(name, incom, position);
            team.CaptureStar(star);

            return star;
        }

        public void SetOriginal(Star star)
        {
            name = star.Name;
            incom = star.Incom;
            owner_id = star.Owner.Id;
            position = star.Position + Vector2.zero;
        }
    }
}
