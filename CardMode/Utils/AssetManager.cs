using System;
using System.Collections.Generic;
using System.IO;

using Terraria.ModLoader;

namespace OdeMod.CardMode.Utils
{
    public class AssetManager
    {
        public static AssetManager Instance { get; private set; }
        private Dictionary<string, Stream> _assetsStream;
        private Dictionary<string, object> _assetsInstance;
        private Dictionary<Type, Func<string, Stream, object>> _processingMethods;

        public AssetManager()
        {
            Instance = this;
            _assetsInstance = new Dictionary<string, object>();
            _assetsStream = new Dictionary<string, Stream>();
            _processingMethods = new Dictionary<Type, Func<string, Stream, object>>();
        }

        public Stream GetFileStream(string path)
        {
            path = path.Replace('\\', '/');
            if (path.StartsWith("OdeMod/"))
                path = path.Substring(7, path.Length);
            if (!_assetsStream.ContainsKey(path))
                _assetsStream.Add(path, OdeMod.Instance.GetFileStream(path));
            return _assetsStream[path];
        }

        public void AddProcessingMethod<T>(Func<string, Stream, object> func)
        {
            _processingMethods.Add(typeof(T), func);
        }

        public T Request<T>(string path) where T : class
        {
            path = path.Replace('\\', '/');
            Type type = typeof(T);
            if (_assetsInstance.ContainsKey(path))
                return (T)_assetsInstance[path];
            if (_processingMethods.ContainsKey(type))
            {
                _assetsInstance.Add(path, _processingMethods[type](path, GetFileStream(path)));
            }
            else
            {
                _assetsInstance.Add(path, ModContent.Request<T>(path, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
            }
            return (T)_assetsInstance[path];
        }
    }
}