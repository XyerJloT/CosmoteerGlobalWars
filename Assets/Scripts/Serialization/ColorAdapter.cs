using UnityEngine;

namespace Assets.Scripts.Serialization
{
    public class ColorAdapter : ISerializeAdapter<Color>, IDeserializeAdapter<Color>
    {
        public float r, g, b, a;

        public Color GetOriginal() => new Color(r, g, b, a);

        public void SetOriginal(Color value)
        {
            r = value.r;
            g = value.g;
            b = value.b;
            a = value.a;
        }
    }
}
