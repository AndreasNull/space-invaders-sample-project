using UnityEngine;
using Zenject;
using System;

namespace AGames.SpaceInvaders
{
    public class GameInstaller : MonoInstaller
    {
        #region Inpsector
        [SerializeField] Transform m_WaveTransform;
        #endregion

        [Inject]
        Settings m_Settings = null;

        public override void InstallBindings()
        {
            // Install Projectiles
            Container.BindFactory<float, ProjectileTypes, ProjectileMono, ProjectileMono.Factory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<float, ProjectileTypes, ProjectileMono, ProjectilePool>(poolBinder => poolBinder
                    .WithInitialSize(50)
                    //.FromComponentInNewPrefab(AssetManager.GetAssetComponent<ProjectileMono>(m_Settings.projectileAssetPath))
                    .FromAssetManager(m_Settings.projectileAssetPath)
                    .UnderTransformGroup("Projectiles")); ;

            // Install Enemies
            Container.BindFactory<EnemyConfig, int, EnemyMono, EnemyMono.Factory>()
                .FromPoolableMemoryPool<EnemyConfig, int, EnemyMono, EnemyPool>(poolBinder => poolBinder
                   .WithInitialSize(55)
                   .FromAssetManager(m_Settings.enemyAssetPath)
                   //.FromComponentInNewPrefab(AssetManager.GetAssetComponent<EnemyMono>(m_Settings.enemyAssetPath))
                   .UnderTransform(m_WaveTransform));

            // Install Game State Manager & States
            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InitGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<InGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverState>().AsSingle();

            // Install Game Stats
            Container.Bind<GameStatsHandler>().AsSingle();

        }

        // We could just use FromMonoPoolableMemoryPool above, but we have to use these instead
        // for IL2CPP to work
        class ProjectilePool : MonoPoolableMemoryPool<float, ProjectileTypes, IMemoryPool, ProjectileMono> { }
        class EnemyPool : MonoPoolableMemoryPool<EnemyConfig, int, IMemoryPool, EnemyMono> { }

        #region Settings
        [Serializable]
        public class Settings
        {
            [SerializeField] string m_ProjectileAssetPath;
            [SerializeField] string m_EnemyAssetPath;

            public string projectileAssetPath => m_ProjectileAssetPath;
            public string enemyAssetPath => m_EnemyAssetPath;
        }
        #endregion
    }
}