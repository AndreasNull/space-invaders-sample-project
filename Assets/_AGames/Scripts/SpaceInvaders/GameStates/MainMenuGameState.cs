using AGames.StateMachine;
using UnityEngine;

namespace AGames.SpaceInvaders
{
    public class MainMenuGameState : IState
    {
        // dependeces
        readonly PlayerMono m_Player;
        readonly EnemyWaveMono m_EnemyWave;
        readonly GameStatsHandler m_GameStatsHandler;

        public MainMenuGameState(PlayerMono player, EnemyWaveMono enemyWave, GameStatsHandler gameStatsHandler)
        {
            m_Player = player;
            m_EnemyWave = enemyWave;
            m_GameStatsHandler = gameStatsHandler;
        }

        public void OnEnter()
        {
            UnityEngine.Debug.Log("MainMenuGameState::OnEnter");
            // update high score
            m_GameStatsHandler.BroadcastHighScore();
            // show main menu panel
            GameMessage.Ui.ShowMainMenu.Post();
            m_Player.Lock();
            m_EnemyWave.Clear();
        }

        public void OnExit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }
    }
    
}