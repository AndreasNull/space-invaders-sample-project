using UnityEngine;
using UnityEngine.UI;

namespace AGames.SpaceInvaders.UI
{
    public class HUDManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField] Button m_ExitButton;
        [SerializeField] TMPro.TextMeshProUGUI m_LivesTMPro;
        [SerializeField] TMPro.TextMeshProUGUI m_ScoreTMPro;
        [SerializeField] TMPro.TextMeshProUGUI m_WaveTMPro;
        #endregion

        private void Awake()
        {
            GameMessage.Ui.PlayerLivesUpdated.AddObserver<int>(OnPlayerLivesUpdated);
            GameMessage.Ui.PlayerScoreUpdated.AddObserver<int>(OnPlayerScoreUpdated);
            GameMessage.Ui.EnemyWavesUpdated.AddObserver<int>(OnEnemyWavesUpdated);
            // main menu button
            m_ExitButton.onClick.AddListener(() => GameMessage.StateCommand.Post(GameStateCommand.GoToMainMenu));
        }

        private void OnDestroy()
        {
            GameMessage.Ui.PlayerLivesUpdated.RemoveObserver<int>(OnPlayerLivesUpdated);
            GameMessage.Ui.PlayerScoreUpdated.RemoveObserver<int>(OnPlayerLivesUpdated);
            GameMessage.Ui.EnemyWavesUpdated.RemoveObserver<int>(OnEnemyWavesUpdated);
        }

        void OnPlayerScoreUpdated(int score) => m_ScoreTMPro.text = score.ToString();

        void OnPlayerLivesUpdated(int lives) => m_LivesTMPro.text = lives.ToString();
        void OnEnemyWavesUpdated(int lives) => m_WaveTMPro.text = lives.ToString();
    }
}