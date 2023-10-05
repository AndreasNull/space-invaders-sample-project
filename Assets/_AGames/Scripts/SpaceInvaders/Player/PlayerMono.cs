using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerMono : MonoBehaviour
    {
        // dependences
        Player m_Player;
        PlayerHealthHandler m_HealthHandler;

        private Vector3 m_InitPosition;

        [Inject]
        public void Construct(Player player, PlayerHealthHandler healthHandler)
        {
            m_Player = player;
            m_HealthHandler = healthHandler;
        }

        public void TakeDamage() => m_HealthHandler.TakeDamage(1);

        public void Lock()
        {
            m_Player.isLocked = true;
        }

        public void Reset()
        {
            transform.position = m_InitPosition;
            m_HealthHandler.ResetLives();
            m_Player.isLocked = false;
        }

        private void Awake()
        {
            m_InitPosition = transform.position;
        }
    }
}