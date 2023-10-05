using AGames.StateMachine;
using UnityEngine;

namespace AGames.SpaceInvaders
{
    public partial class EnemyWaveMoveHandler
    {
        public class IdleState : IState
        {
            public void OnEnter() => Debug.Log("EnemyWaveMoveHandler.MoveState::IdleState");
            public void OnExit() { }
            public void OnFixedUpdate() { }
            public void OnUpdate() { }
        }
    }
}