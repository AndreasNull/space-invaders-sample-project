using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AGames.SpaceInvaders
{
    [CreateAssetMenu(menuName = "Space Invaders/Game Loader Config")]
    public class GameLoaderConfig : ScriptableObject
    {
        [SerializeField] float m_WaitAfterLoadingEverything;
        [SerializeField] AssetLabelReference m_AssetLabelToPreload;

        public float WaitAfterLoadingEverything => m_WaitAfterLoadingEverything;
        public string PreloadKey => m_AssetLabelToPreload.labelString;
    }
}