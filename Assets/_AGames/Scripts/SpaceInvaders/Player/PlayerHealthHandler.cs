using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerHealthHandler : ITickable
    {
        Player m_Player;
        Settings m_Settings;

        int m_Lives;

        public PlayerHealthHandler(Player player, Settings settings)
        {
            m_Player = player;
            m_Settings = settings;
        }

        public void ResetLives()
        {
            SetLives(m_Settings.maxLives);
            DisableInvulnerableFX();
        }

        public void TakeDamage(int damage)
        {
            if (isInvunerable)
                return;

            SetLives(Mathf.Max(0, m_Lives - damage));

            if (m_Lives == 0)
            {
                Debug.Log("--- Game Over ---");
                GameMessage.StateCommand.Post(GameStateCommand.GameOver);
            }
            else
            {
                // enable invunerable fx & start timer
                m_Player.EnableInvunerableFX();
                    m_InvulnerableTimer = m_Settings.invulnerableDuretion;
            }
        }

        #region Implement Invunerability
        private float m_InvulnerableTimer;
        private bool isInvunerable => m_InvulnerableTimer > 0;

        private void DisableInvulnerableFX()
        {
            // disable fx
            m_Player.DisableInvunerableFX();
            // stop timer
            m_InvulnerableTimer = -2;
        }

        public void Tick()
        {
            // check if timer is stopped
            if (m_InvulnerableTimer < -1)
                return;

            m_InvulnerableTimer -= Time.deltaTime;

            if (m_InvulnerableTimer < 0)
                DisableInvulnerableFX();
        }
        #endregion


        private void SetLives(int lives)
        {
            m_Lives = lives;
            GameMessage.Ui.PlayerLivesUpdated.Post(m_Lives);
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] int m_MaxLives = 3;
            [SerializeField] float m_InvulnerableDuretion = 3;
            public int maxLives => m_MaxLives;
            public float invulnerableDuretion => m_InvulnerableDuretion;
        }

    }
}