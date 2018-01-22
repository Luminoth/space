using EnergonSoftware.Core.Util;

using JetBrains.Annotations;

using UnityEngine;

namespace EnergonSoftware.Core.Assets
{
    public sealed class AssetManager : SingletonBehavior<AssetManager>
    {
        [CanBeNull]
        public T LoadAsset<T>(string path) where T: UnityEngine.Object
        {
            string fullPath = $"Assets/Data/base/{path}";
            //Debug.Log($"Loading asset from {fullPath}...");
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(fullPath);
        }

        [CanBeNull]
        public GameObject LoadPrefab(string path)
        {
            return LoadAsset<GameObject>(path);
        }

        [CanBeNull]
        public GameObject LoadAndInstantiatePrefab(string path)
        {
            GameObject prefab = LoadPrefab(path);
            return Instantiate(prefab);
        }
    }
}
