using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerShooterHandler : ITickable
    {
        // dependences
        readonly Player m_Player;
        readonly Settings m_Settings;
        readonly ProjectileMono.Factory m_BulletFactory;
        readonly PlayerInputState m_InputState;

        private float m_LastFireTime;

        // constructor
        public PlayerShooterHandler(Player player, Settings settings, ProjectileMono.Factory bulletFactory, PlayerInputState inputState)
        {
            m_Player = player;
            m_Settings = settings;
            m_BulletFactory = bulletFactory;
            m_InputState = inputState;
        }

        #region Implement ITckable
        public void Tick()
        {
            if (m_Player.isLocked)
                return;

            if (m_InputState.isFiring && Time.realtimeSinceStartup - m_LastFireTime > m_Settings.maxShootInterval)
            {
                m_LastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }
        #endregion

        private void Fire()
        {
            var bullet = m_BulletFactory.Create(m_Settings.speed, ProjectileTypes.FromPlayer);
            bullet.transform.position = m_Player.position + m_Player.forward * m_Settings.offsetDistance;
            bullet.transform.rotation = m_Player.rotation;
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] float m_Speed;
            [SerializeField] float m_OffsetDistance;
            [SerializeField] float m_MaxShootInterval;

            public float speed => m_Speed;
            public float offsetDistance => m_OffsetDistance;
            public float maxShootInterval => m_MaxShootInterval;
        }
    }
}