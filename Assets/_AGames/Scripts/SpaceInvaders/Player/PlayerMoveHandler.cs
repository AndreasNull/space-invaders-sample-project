using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerMoveHandler : IFixedTickable
    {
        // Dependencies
        readonly Player m_Player;
        readonly Settings m_Settings;
        readonly PlayerInputState m_InputState;

        public PlayerMoveHandler(PlayerInputState inputState, Player player, Settings settings)
        {
            m_Player = player;
            m_InputState = inputState;
            m_Settings = settings;
        }

        public void FixedTick()
        {
            if (m_Player.isLocked)
                return;

            if (m_InputState.isMovingLeft)
                m_Player.Move(Vector3.left * m_Settings.moveSpeed);

            if (m_InputState.isMovingRight)
                m_Player.Move(Vector3.right * m_Settings.moveSpeed);
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] float m_MoveSpeed;

            public float moveSpeed => m_MoveSpeed;
        }
    }
}