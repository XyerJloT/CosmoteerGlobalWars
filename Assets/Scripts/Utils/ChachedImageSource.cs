using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class ChachedImageSource : IImageSource
    {
        private readonly Dictionary<string, Texture2D> _cache = new Dictionary<string, Texture2D>();

        public void ResetCache()
        {
            _cache.Clear();
        }

        public void ResetCacedPath(string path)
        {
            _cache.Remove(path);
        }

        public Texture2D Load(string path)
        {
            if (_cache.ContainsKey(path) && File.GetLastWriteTime(path) < System.DateTime.Now)
            {
                return _cache[path];
            }

            byte[] data = File.ReadAllBytes(path);

            var texture = new Texture2D(0, 0);
            texture.LoadImage(data);

            _cache[path] = texture;
            return texture;
        }
    }
}
