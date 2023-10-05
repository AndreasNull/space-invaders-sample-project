using AGames.StateMachine;
using UnityEngine;
using Zenject;
using System;

namespace AGames.SpaceInvaders
{
    public partial class EnemyWaveMoveHandler
    {
        public class MoveState : IState, IInitializable, IDisposable
        {
            readonly EnemyWave m_EnemyWave;
            readonly EnemyWaveMoveHandler.Settings m_Settings;

            float m_MoveInterval;
            float m_Timer;
            float m_Direction = 1;

            public MoveState(EnemyWave enemyWave, EnemyWaveMoveHandler.Settings settings)
            {
                m_EnemyWave = enemyWave;
                m_Settings = settings;
            }
            public void Dispose()
            {
                GameMessage.Gameplay.EnemyLimit.RemoveObserver<EnemyWaveLimiterMono.Type>(OnEnemyLimit);
            }

            public void Initialize()
            {
                // observe message
                GameMessage.Gameplay.EnemyLimit.AddObserver<EnemyWaveLimiterMono.Type>(OnEnemyLimit);
            }

            public void OnEnter() 
            {
                Debug.Log("EnemyWaveMoveHandler.MoveState::OnEnter");
                // init position
                m_EnemyWave.InitPosition();
                // reset direction
                m_Direction = 1;
                // reset move interval
                m_MoveInterval = m_Settings.moveInterval;
                // reset timer
                m_Timer = m_Settings.moveInterval;
            }

            public void OnExit() { }

            public void OnUpdate()
            {
                m_Timer -= Time.deltaTime;

                if (m_Timer < 0)
                {
                    m_Timer = m_MoveInterval;
                    m_EnemyWave.Move(Vector3.right * m_Direction * m_Settings.moveHorizontalDistance);
                }
            }

            public void OnFixedUpdate() { }

            private void OnEnemyLimit(EnemyWaveLimiterMono.Type type)
            {
                switch (type)
                {
                    case EnemyWaveLimiterMono.Type.Left:
                        m_Direction = 1;
                        break;

                    case EnemyWaveLimiterMono.Type.Right:
                        m_Direction = -1;
                        break;
                }

                m_EnemyWave.Move(Vector3.back * m_Settings.moveVerticalDistance);
                m_MoveInterval -= m_MoveInterval * m_Settings.moveIntervalDecreasePercent;
                m_Timer = m_MoveInterval;
            }
        }
    }
}