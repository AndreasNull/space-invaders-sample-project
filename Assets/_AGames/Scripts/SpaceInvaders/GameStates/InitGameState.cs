using AGames.StateMachine;

namespace AGames.SpaceInvaders
{
    public class InitGameState : IState
    {
        // dependences
        readonly GameStatsHandler m_GameStatsHandler;

        public InitGameState(GameStatsHandler gameStatsHandler)
        {
            m_GameStatsHandler = gameStatsHandler;
        }

        public void OnEnter() 
        { 
            UnityEngine.Debug.Log("InitGameState::OnEnter");
            
            // restore data
            m_GameStatsHandler.Restore();

            // We can perform here more game init stuff
            // ...
        }
        public void OnExit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }
    }
}