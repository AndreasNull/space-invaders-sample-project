using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AGames.AssetManagment
{
    public static class AssetManager
    {
        public struct AssetInfo
        {
            public string path { get; set; }
            public Type assetType { get; set; }
        }

        private static Dictionary<string, object> s_Assets = new Dictionary<string, object>();
        private static Queue<AssetInfo> s_AssetsToLoad = new Queue<AssetInfo>();

        public static T GetAsset<T>(string path)
        {
            if (s_Assets.ContainsKey(path))
                return (T)s_Assets[path];
            return default(T);
        }

        public static T GetAssetComponent<T>(string path) where T : Component
        {
            var asset = GetAsset<GameObject>(path);

            if (asset != null)
                return asset.GetComponent<T>();

            return null;
        }

        public static void RegisterAssetToLoad(string _path, Type _type)
        {
            s_AssetsToLoad.Enqueue(new AssetInfo()
            {
                path = _path,
                assetType = _type
            });
        }

        private static Action s_OnLoadAssetsComplete;

        public static void LoadAssets(Action onComplete)
        {
            s_OnLoadAssetsComplete = onComplete;
            LoadNextAssetFromQueue();
        }

        static Queue<IResourceLocation> s_ResourceLocationQueue;
        static int s_AssetsCount;
        static int s_AssetDownloadIndex;
        static Action<float> s_OnProgressUpdated;

        public static void LoadAssets(string key, Action onComplete, Action<float> onProgressUpdated)
        {
            s_OnLoadAssetsComplete = onComplete;
            s_OnProgressUpdated = onProgressUpdated;

            Addressables.LoadResourceLocationsAsync(key).Completed += (op) =>
            {
                s_ResourceLocationQueue = new Queue<IResourceLocation>(op.Result);
                s_AssetsCount = op.Result.Count;
                s_AssetDownloadIndex = 0;
                ProcessNextResourceLocationFromQueue();
            };
        }

        private static void ProcessNextResourceLocationFromQueue()
        {
            if (s_ResourceLocationQueue.Count == 0)
            {
                s_OnLoadAssetsComplete?.Invoke();
                return;
            }

            var location = s_ResourceLocationQueue.Dequeue();

            //Debug.Log($"{location.InternalId} .. {location.ResourceType}");

            // is game object
            if (location.ResourceType == typeof(UnityEngine.GameObject))
            {
                LoadAsset<GameObject>(location, (x) => ProcessNextResourceLocationFromQueue());
            }
            
            // is scene
            else if(location.ResourceType == typeof(SceneInstance))
            {
                Addressables.LoadSceneAsync(location, LoadSceneMode.Additive, true)
                    .Completed += (x) => ProcessNextResourceLocationFromQueue();
            }
            else
            {
                //throw new Exception($"Asset Manager can not handle {location.ResourceType} asset type.");
                // skip

                s_AssetDownloadIndex++;
                s_OnProgressUpdated(s_AssetDownloadIndex / s_AssetsCount);
                ProcessNextResourceLocationFromQueue();
                return;

            }

            s_AssetDownloadIndex++;
            s_OnProgressUpdated(s_AssetDownloadIndex / s_AssetsCount);

        }

        private static void LoadNextAssetFromQueue()
        {
            if (s_AssetsToLoad.Count == 0)
            {
                s_OnLoadAssetsComplete?.Invoke();
                return;
            }

            var assetInfo = s_AssetsToLoad.Dequeue();
            LoadAsset<GameObject>(assetInfo.path, (x) => LoadNextAssetFromQueue());
        }

        public static void LoadAsset<T>(string path, Action<AsyncOperationStatus> onComplete)
        {
            Addressables.LoadAssetAsync<T>(path).Completed += (x) =>
            {
                //Debug.Log($"Asset {path} loaded.");
                AddAsset(path, x.Result);
                onComplete(x.Status);
            };
        }

        public static void LoadAsset<T>(IResourceLocation location, Action<AsyncOperationStatus> onComplete)
        {
            Addressables.LoadAssetAsync<T>(location).Completed += (x) =>
            {
                //Debug.Log($"Asset {location} loaded.");
                AddAsset(location.PrimaryKey, x.Result);
                onComplete(x.Status);
            };
        }

        private static void AddAsset(string path, object asset)
        {
            if (s_Assets.ContainsKey(path))
                s_Assets[path] = asset;
            else
                s_Assets.Add(path, asset);
        }
    }
}