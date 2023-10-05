using UnityEngine;

namespace AGames.SpaceInvaders
{
    public class GameObjectPlaceholder : MonoBehaviour
    {
        [SerializeField] string m_ModelAssetReferencePath;

        private void Awake()
        {
            Instantiate(AssetManagment.AssetManager.GetAsset<GameObject>(m_ModelAssetReferencePath), this.transform);
        }
    }
}