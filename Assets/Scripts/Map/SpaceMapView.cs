using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class SpaceMapView : MonoBehaviour
    {
        [SerializeField] private StarView _starPrefab;
        [Tooltip("Родитель для отрисовки звезд")]
        [SerializeField] private Transform _starContainer;

        [SerializeField] private TunnelView _tunnelPrefab;
        [Tooltip("Родитель для отрисовки тоннелей")]
        [SerializeField] private Transform _tunnelContainer;


        private readonly List<StarView> _stars = new List<StarView>();
        private readonly List<TunnelView> _tunnels = new List<TunnelView>();

        public void Init(SpaceMap space)
        {
            foreach (var star in space.Stars)
            {
                var starView = CreateStarView(star);
                _stars.Add(starView);

                _tunnels.AddRange(space.GetTunnelsOnStar(star).Select(CreateTunnelView));
            }
        }

        private StarView CreateStarView(Star star)
        {
            var view = Instantiate(_starPrefab, _starContainer);
            view.Init(star);
            return view;
        }

        private TunnelView CreateTunnelView(Tunnel tunnel)
        {
            var view = Instantiate(_tunnelPrefab, _tunnelContainer);
            view.Init(tunnel);
            return view;
        }
    }
}
