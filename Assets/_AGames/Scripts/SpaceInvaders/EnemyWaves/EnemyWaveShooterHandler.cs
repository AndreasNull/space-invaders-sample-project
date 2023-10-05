using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class EnemyWaveShooterHandler : ITickable
    {
        // dependences
        readonly EnemyWaveRegistry m_EnemyWaveRegistry;
        readonly ProjectileMono.Factory m_ProjectileFactory;
        readonly Settings m_Settings;
        

        private float m_Timer;

        public EnemyWaveShooterHandler(EnemyWaveRegistry enemyWaveRegistry, ProjectileMono.Factory factory, Settings settings)
        {
            m_EnemyWaveRegistry = enemyWaveRegistry;
            m_ProjectileFactory = factory;
            m_Settings = settings;
        }

        #region Implement ITckable
        public void Tick()
        {
            if (m_EnemyWaveRegistry.enemiesCount == 0)
                return;

            m_Timer -= Time.deltaTime;

            if(m_Timer < 0)
            {
                m_Timer = m_Settings.shootInterval;
                Fire();
            }
        }
        #endregion

        private void Fire()
        {
            EnemyMono enemy = m_EnemyWaveRegistry.GetRandomBottomEnemy();
            //Debug.Log(enemy.id);
            var projectile = m_ProjectileFactory.Create(m_Settings.speed, ProjectileTypes.FromEnemy);
            projectile.transform.position = enemy.transform.position + enemy.transform.forward * m_Settings.offsetDistance;
            projectile.transform.rotation = enemy.transform.rotation;
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] float m_Speed;
            [SerializeField] float m_OffsetDistance;
            [Header("Random Shoot Interval")]
            [SerializeField] float m_MinShootInterval;
            [SerializeField] float m_MaxShootInterval;

            public float speed => m_Speed;
            public float offsetDistance => m_OffsetDistance;
            public float shootInterval => UnityEngine.Random.Range(m_MinShootInterval, m_MaxShootInterval);
        }
    }
}