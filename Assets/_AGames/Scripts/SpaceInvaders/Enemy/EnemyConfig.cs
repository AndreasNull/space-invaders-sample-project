using AGames.AssetManagment;
using UnityEngine;

namespace AGames.SpaceInvaders
{
    [CreateAssetMenu(menuName = "Space Invaders/Enemy Config")]
    [System.Serializable]
    public class EnemyConfig : ScriptableObject
    {

        [SerializeField] string m_MeshAssetPath;
        [SerializeField] int m_Points;

        // cache Mesh
        Mesh m_Mesh;

        public Mesh mesh
        {
            get
            {
                if (m_Mesh == null)
                    m_Mesh = AssetManager.GetAssetComponent<MeshFilter>(m_MeshAssetPath).sharedMesh;

                return m_Mesh;
            }
        }

        public int points => m_Points;
    }
}