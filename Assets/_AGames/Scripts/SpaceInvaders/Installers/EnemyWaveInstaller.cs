using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class EnemyWaveInstaller : MonoInstaller
    {
        [SerializeField] Settings m_Settings = null;

        public override void InstallBindings()
        {
            Container.Bind<EnemyWave>().AsSingle().WithArguments(m_Settings.transform);
            Container.Bind<EnemyWaveSpawnHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyWaveRegistry>().AsSingle();

            Container.BindInterfacesTo<EnemyWaveMoveHandler>().AsSingle();
            Container.BindInterfacesTo<EnemyWaveShooterHandler>().AsSingle();

            // EnemyWaveMoveHandler States
            Container.BindInterfacesAndSelfTo<EnemyWaveMoveHandler.IdleState>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyWaveMoveHandler.MoveState>().AsSingle();
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] Transform m_Transform;

            public Transform transform => m_Transform;
        }
    }
}