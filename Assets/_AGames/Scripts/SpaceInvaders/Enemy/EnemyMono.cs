using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    [RequireComponent(typeof(MeshFilter))]
    public class EnemyMono : MonoBehaviour, IPoolable<EnemyConfig, int, IMemoryPool>
    {
        IMemoryPool m_Pool;
        MeshFilter m_MeshFilter;
        
        public int id { get; private set; }
        public int points { get; private set; }

        #region Impement IPoolable
        public void OnDespawned() => m_Pool = null;

        public void OnSpawned(EnemyConfig config, int id, IMemoryPool pool)
        {
            m_Pool = pool;

            // cache mesh filter
            if (m_MeshFilter == null)
                m_MeshFilter = GetComponent<MeshFilter>();

            m_MeshFilter.mesh = config.mesh;
            points = config.points;
            this.id = id;
        }
        #endregion

        public void Despawn () => m_Pool?.Despawn(this);

        public void Kill()
        {
            GameMessage.Gameplay.EnemyKilled.Post(this);
            Despawn();
        }

        public class Factory : PlaceholderFactory<EnemyConfig, int, EnemyMono> { }
    }
}