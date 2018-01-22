using EnergonSoftware.Core.Util;

using UnityEngine;

namespace EnergonSoftware.Core.Assets
{
    public sealed class AssetManager : SingletonBehavior<AssetManager>
    {
        public T LoadAsset<T>(string path) where T: UnityEngine.Object
        {
            string fullPath = $"Assets/Data/base/{path}";
            //Debug.Log($"Loading asset from {fullPath}...");
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(fullPath);
        }

        public GameObject LoadPrefab(string path)
        {
            return LoadAsset<GameObject>(path);
        }

        public GameObject LoadAndInstantiatePrefab(string path)
        {
            GameObject prefab = LoadPrefab(path);
            return Instantiate(prefab);
        }
    }
}
