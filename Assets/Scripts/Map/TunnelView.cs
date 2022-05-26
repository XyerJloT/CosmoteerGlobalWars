using UnityEngine;

namespace Assets.Scripts.Map
{
    public class TunnelView : MonoBehaviour
    {
        public void Init(Tunnel tunnel)
        {
            var from = tunnel.From.Position;
            var to = tunnel.To.Position;

            var position = (from + to) / 2;
            var angle = Vector2.Angle(from, to);

            gameObject.transform.position = position;
            
            var rotation = gameObject.transform.eulerAngles;
            rotation.z = angle;
            gameObject.transform.eulerAngles = rotation;

            //TODO: растягивание тоннеля
        }
    }
}
