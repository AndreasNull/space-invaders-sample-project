using AGames.StateMachine;

namespace AGames.SpaceInvaders
{
    public class GameOverState : IState
    {
        // dependences
        readonly PlayerMono m_Player;
        readonly EnemyWaveMono m_EnemyWave;
        readonly GameStatsHandler m_GameStatsHandler;

        public GameOverState(PlayerMono player, EnemyWaveMono enemyWave, GameStatsHandler gameStatsHandler)
        {
            m_Player = player;
            m_EnemyWave = enemyWave;
            m_GameStatsHandler = gameStatsHandler;
        }

        public void OnEnter() 
        {
            // save score
            m_GameStatsHandler.AddScore(m_EnemyWave.score);
            // show results screen
            GameMessage.Ui.ShowResults.Post(m_EnemyWave.score, m_EnemyWave.waves);
            // lock player
            m_Player.Lock();
            // clear wave
            m_EnemyWave.Clear();
        }

        public void OnExit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }
    }
}