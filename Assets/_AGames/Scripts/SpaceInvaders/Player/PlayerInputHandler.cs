using System;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class PlayerInputHandler : ITickable, IInitializable, IDisposable
    {
        // dependences
        readonly PlayerInputState m_InputState;

        private bool m_IsMovingLeft, m_IsMovingRight, m_IsFiring;

        public PlayerInputHandler(PlayerInputState inputState)
        {
            m_InputState = inputState;
        }

        public void Initialize()
        {
            GameMessage.Input.Left.AddObserver<bool>(OnLeft);
            GameMessage.Input.Right.AddObserver<bool>(OnRight);
            GameMessage.Input.Fire.AddObserver<bool>(OnFire);
        }

        public void Dispose()
        {
            GameMessage.Input.Left.RemoveObserver<bool>(OnLeft);
            GameMessage.Input.Right.RemoveObserver<bool>(OnRight);
            GameMessage.Input.Fire.RemoveObserver<bool>(OnFire);
        }

        private void OnLeft(bool value) => m_IsMovingLeft = value;
        private void OnRight(bool value) => m_IsMovingRight = value;
        private void OnFire(bool value) => m_IsFiring = value;

        public void Tick()
        {
            m_InputState.isMovingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || m_IsMovingLeft;
            m_InputState.isMovingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || m_IsMovingRight;
            m_InputState.isFiring = Input.GetKey(KeyCode.Space) | m_IsFiring;
        }
    }
}