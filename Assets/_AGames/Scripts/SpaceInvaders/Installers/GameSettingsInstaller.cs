using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    [CreateAssetMenu(menuName = "Space Invaders/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [Header("General Settings")]
        [SerializeField] GameInstaller.Settings m_GameInstaller;
        [Header("Game Stats Settings")]
        [SerializeField] GameStatsHandler.Settings m_GameStats;
        [Header("Player Settings")]
        [SerializeField] PlayerSettings m_PlayerSettings;
        [Header("Enemy Settings")]
        [SerializeField] EnemySettings m_EnemySettings;

        [System.Serializable]
        public class PlayerSettings
        {
            [SerializeField] PlayerHealthHandler.Settings m_HealthHandler;
            [SerializeField] PlayerMoveHandler.Settings m_PlayerMoveHandler;
            [SerializeField] PlayerShooterHandler.Settings m_ShooterHanlder;

            public PlayerHealthHandler.Settings healthHandler => m_HealthHandler;
            public PlayerMoveHandler.Settings moveHandler => m_PlayerMoveHandler;
            public PlayerShooterHandler.Settings shooterHanlder => m_ShooterHanlder;
        }

        [System.Serializable]
        public class EnemySettings
        {
            [SerializeField] EnemyWaveSpawnHandler.Settings m_SpawnHandler;
            [SerializeField] EnemyWaveMoveHandler.Settings m_MoveHandler;
            [SerializeField] EnemyWaveShooterHandler.Settings m_ShooterHandler;

            public EnemyWaveSpawnHandler.Settings spawnHandler => m_SpawnHandler;
            public EnemyWaveMoveHandler.Settings moveHandler => m_MoveHandler;
            public EnemyWaveShooterHandler.Settings shooterHandler => m_ShooterHandler;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(m_GameInstaller).IfNotBound();
            Container.BindInstance(m_GameStats).IfNotBound();
            Container.BindInstance(m_PlayerSettings.healthHandler).IfNotBound();
            Container.BindInstance(m_PlayerSettings.moveHandler).IfNotBound();
            Container.BindInstance(m_PlayerSettings.shooterHanlder).IfNotBound();
            Container.BindInstance(m_EnemySettings.spawnHandler).IfNotBound();
            Container.BindInstance(m_EnemySettings.moveHandler).IfNotBound();
            Container.BindInstance(m_EnemySettings.shooterHandler).IfNotBound();
        }
    }
}