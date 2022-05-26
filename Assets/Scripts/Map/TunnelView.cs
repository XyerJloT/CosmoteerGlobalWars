using UnityEngine;

namespace Assets.Scripts.Map
{
    [RequireComponent(typeof(RectTransform))]
    public class TunnelView : MonoBehaviour
    {
        public void Init(Tunnel tunnel)
        {
            var from = tunnel.From.Position;
            var to = tunnel.To.Position;

            var position = (from + to) / 2;
            var angle = Vector2.Angle(Vector2.up, to - from);
            var length = (to - from).magnitude;

            gameObject.transform.position = position;
            
            var rotation = gameObject.transform.eulerAngles;
            rotation.z = angle;
            gameObject.transform.eulerAngles = rotation;

            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, length);
            rectTransform.ForceUpdateRectTransforms();
        }
    }
}
