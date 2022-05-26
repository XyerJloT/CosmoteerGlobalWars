using UnityEngine;

namespace Assets.Scripts.Serialization
{
    class Vector2Adapter : ISerializeAdapter<Vector2>, IDeserializeAdapter<Vector2>
    {
        public float x, y;

        public Vector2 GetOriginal() => new Vector2(x, y);

        public void SetOriginal(Vector2 value)
        {
            x = value.x;
            y = value.y;
        }
    }
}
