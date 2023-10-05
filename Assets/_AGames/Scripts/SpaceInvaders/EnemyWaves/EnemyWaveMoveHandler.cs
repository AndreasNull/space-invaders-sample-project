using AGames.StateMachine;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public partial class EnemyWaveMoveHandler : SimpleStateMachine
    {
        // dependences
        readonly EnemyWaveRegistry m_EnemyWaveRegistry;

        public enum State { Idle, Move }

        public EnemyWaveMoveHandler(EnemyWaveRegistry enemyWaveRegistry, IdleState idleState, MoveState moveState)
        {
            m_EnemyWaveRegistry = enemyWaveRegistry;

            // add states
            AddState(State.Idle, idleState); // init state
            AddState(State.Move, moveState);
            // add transitions
            AddTransition(State.Idle, State.Move, new FuncCondition(HasEnemies));
            AddTransition(State.Move, State.Idle, new FuncCondition(HasNoEnemies));
            // start machine
            StartStateMachine();
        }

        private bool HasEnemies() => m_EnemyWaveRegistry.enemiesCount > 0;
        private bool HasNoEnemies() => m_EnemyWaveRegistry.enemiesCount == 0;

        [System.Serializable]
        public class Settings
        {
            [SerializeField] float m_MoveInterval = 0.5f;
            [SerializeField, Range(0f, 1f)] float m_MoveIntervalDecreasePercent = 0.2f;
            [SerializeField] float m_MoveHorizontalDistance = 0.5f;
            [SerializeField] float m_MoveVerticalDistance = 1f;

            public float moveInterval => m_MoveInterval;
            public float moveIntervalDecreasePercent => m_MoveIntervalDecreasePercent;
            public float moveHorizontalDistance => m_MoveHorizontalDistance;
            public float moveVerticalDistance => m_MoveVerticalDistance;
        }
    }
}