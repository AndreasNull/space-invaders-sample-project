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
    [Serializable]
    public class GameObjectReference : AssetReference
    {
        [SerializeField] string m_AssetPath;
    }
}
