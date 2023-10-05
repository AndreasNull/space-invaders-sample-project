#if !NOT_UNITY3D

using ModestTree;
using AGames.AssetManagment;

namespace Zenject
{
    /// <summary>
    /// Custom provider class to support AGames.AssetManagment.AssetManager (Andreas Diktyopoulos)
    /// </summary>
    [NoReflectionBaking]
    public class AssetManagerProvider : IPrefabProvider
    {
        readonly UnityEngine.Object _prefab;

        public AssetManagerProvider(string path)
        {
            Assert.IsNotNull(path);
            Assert.IsNotEmpty(path);
            _prefab = AssetManager.GetAsset<UnityEngine.GameObject>(path);
        }

        public UnityEngine.Object GetPrefab(InjectContext _)
        {
            return _prefab;
        }
    }
}

#endif


