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

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [Serializable]
    public class SceneReference : AssetReference
    {
        [SerializeField]
        private string sceneName = string.Empty;

        public string SceneName
        {
            get { return sceneName; }
        }

#if UNITY_EDITOR
        public SceneReference(SceneAsset scene)
        : base(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(scene)))
        {
            sceneName = scene.name;
        }

        public override bool ValidateAsset(string path)
        {
            return ValidateAsset(AssetDatabase.LoadAssetAtPath<SceneAsset>(path));
        }

        public override bool ValidateAsset(UnityEngine.Object obj)
        {
            return (obj != null) && (obj is SceneAsset);
        }

        public override bool SetEditorAsset(UnityEngine.Object value)
        {
            if (!base.SetEditorAsset(value))
            {
                return false;
            }

            if (value is SceneAsset scene)
            {
                sceneName = scene.name;
                return true;
            }
            else
            {
                sceneName = string.Empty;
                return false;
            }
        }

#endif
    }
}