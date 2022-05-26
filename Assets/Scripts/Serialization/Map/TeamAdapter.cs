using UnityEngine;

namespace Assets.Scripts.Serialization.Map
{
    public class TeamAdapter : ISerializeAdapter<Team>, IDeserializeAdapter<Team>
    {
        public Team.TeamId id;
        public Color color;

        public Team GetOriginal() => new Team(id, color);

        public void SetOriginal(Team value)
        {
            id = value.Id;
            color = value.Color * 1;
        }
    }
}
