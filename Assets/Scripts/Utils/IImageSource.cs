using UnityEngine;

namespace Assets.Scripts.Utils
{
    public interface IImageSource
    {
        public Texture2D Load(string path);
    }
}
