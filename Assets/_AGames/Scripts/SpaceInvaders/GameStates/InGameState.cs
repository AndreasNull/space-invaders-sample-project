using Cysharp.Threading.Tasks;
using AGames.StateMachine;
using System;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class InGameState : IState, IInitializable, IDisposable
    {
        // cache timespan
        readonly TimeSpan ONE_SECOND_TIME_SPAN = TimeSpan.FromSeconds(1);

        // dependeces
        PlayerMono m_Player;
        EnemyWaveMono m_EnemyWave;

        readonly string[] WELL_DONE_MESSAGES = new string[]
        {
            "well done", "amazing", "superb", "fantastic", "hurray"
        };

        public InGameState(PlayerMono player, EnemyWaveMono enemyWave)
        {
            m_Player = player; 
            m_EnemyWave = enemyWave;
        }

        private void OnAllEnemiesKilled()
        {
            // pop up message
            GameMessage.Ui.InGamePopup.Post(WELL_DONE_MESSAGES[UnityEngine.Random.Range(0, WELL_DONE_MESSAGES.Length)]);
            // win message
            UniTask.Run(WinMessage);
        }

        async UniTaskVoid WinMessage ()
        {
            await UniTask.SwitchToMainThread();
            // pop up message
            GameMessage.Ui.InGamePopup.Post(WELL_DONE_MESSAGES[UnityEngine.Random.Range(0, WELL_DONE_MESSAGES.Length)]);
            // wait
            await UniTask.Delay(ONE_SECOND_TIME_SPAN);
            // next wave
            await UniTask.Run(NextWave);
        }

        async UniTaskVoid NextWave()
        {
            await UniTask.SwitchToMainThread();
            // wait
            //await UniTask.Delay(ONE_SECOND_TIME_SPAN);
            //await UniTask.NextFrame();
            // pop up message
            GameMessage.Ui.InGamePopup.Post("3");
            // wait
            await UniTask.Delay(ONE_SECOND_TIME_SPAN);
            // pop up message
            GameMessage.Ui.InGamePopup.Post("2");
            // wait
            await UniTask.Delay(ONE_SECOND_TIME_SPAN);
            // pop up message
            GameMessage.Ui.InGamePopup.Post("1");
            // wait
            await UniTask.Delay(ONE_SECOND_TIME_SPAN);
            // pop up message
            GameMessage.Ui.InGamePopup.Post(""); // tiny hack :)
            // spawn wave
            m_EnemyWave.NextWave();
        }

        public void OnEnter()
        {
            UnityEngine.Debug.Log("InGameState::OnEnter");
            // reset player
            m_Player.Reset();
            // reset waves
            m_EnemyWave.ResetWaves();
            // spawn wave
            UniTask.Run(NextWave);
            // hide results ui
            GameMessage.Ui.HideResults.Post();
        }

        public void OnExit() 
        {
            // lock player
            m_Player.Lock();
        }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }

        public void Initialize()
        {
            GameMessage.Gameplay.AllEnemiesKilled.AddObserver(OnAllEnemiesKilled);
        }

        public void Dispose()
        {
            GameMessage.Gameplay.AllEnemiesKilled.RemoveObserver(OnAllEnemiesKilled);
        }
    }
}