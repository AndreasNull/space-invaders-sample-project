using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public enum ProjectileTypes
    {
        FromEnemy,
        FromPlayer
    }

    public class ProjectileMono : MonoBehaviour, IPoolable<float, ProjectileTypes, IMemoryPool>
    {
        IMemoryPool m_Pool;

        ProjectileTypes m_Type;
        float m_Speed;

        private void OnTriggerEnter(Collider other)
        {
            // We can do this check comparing tags, but I don't like tags :)
            // It's not easy to maintain them in unity editor and remeber that your code depends on correct tagging.
            if (other.GetComponent<ProjectileLimiterMono>()) 
            {
                m_Pool.Despawn(this);
                return;
            }

            // check for enemy
            if (m_Type == ProjectileTypes.FromPlayer)
            {
                var enemy = other.GetComponent<EnemyMono>();

                if (enemy != null)
                {
                    enemy.Kill();
                    m_Pool.Despawn(this);
                    return;
                }
                
                // check for enemy projectile
                var enemyProjectile = other.GetComponent<ProjectileMono>();

                if(enemyProjectile != null && enemyProjectile.m_Type == ProjectileTypes.FromEnemy)
                {
                    m_Pool.Despawn(this);
                    enemyProjectile.Despawn();
                    return;
                }
            }
            // check for player
            else
            {
                var player = other.GetComponent<PlayerMono>();

                if (player != null)
                {
                    player.TakeDamage();
                    m_Pool.Despawn(this);
                }
            }
        }

        void Update() => transform.position += direction * m_Speed;
        Vector3 direction => m_Type == ProjectileTypes.FromEnemy ? Vector3.back : Vector3.forward;

        private void Despawn () => m_Pool.Despawn(this);

        #region Impement IPoolable
        public void OnDespawned()
        {
            m_Pool = null;
        }

        public void OnSpawned(float speed, ProjectileTypes type, IMemoryPool pool)
        {
            m_Type = type;
            m_Speed = speed;
            m_Pool = pool;
        }
        #endregion

        public class Factory : PlaceholderFactory<float, ProjectileTypes, ProjectileMono> { }
    }
}