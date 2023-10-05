using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] Settings m_Settings = null;

        public override void InstallBindings()
        {
            Container.Bind<Player>().AsSingle().WithArguments(m_Settings.camera, m_Settings.transform, m_Settings.invulnerableFX);

            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerShooterHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerHealthHandler>().AsSingle();

            Container.Bind<PlayerInputState>().AsSingle();
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] Camera m_Camera;
            [SerializeField] Transform m_Transform;
            [SerializeField] GameObject m_InvulnerableFX;

            public Camera camera => m_Camera;
            public Transform transform => m_Transform;
            public GameObject invulnerableFX => m_InvulnerableFX;
        }
    }
}