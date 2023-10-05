using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace AGames.SpaceInvaders.UI
{
    public class HighScoresPanelManager : MonoBehaviour
    {
        [SerializeField] GameObject m_Panel;
        [SerializeField] Button m_ExitButton;
        [SerializeField] TMPro.TextMeshProUGUI m_HighScoresText;
        [SerializeField] ScrollRect m_ScrollRect;
        [SerializeField] GameObject m_NoScoresLabel;

        private StringBuilder m_StringBuilder = new StringBuilder();

        private void Awake()
        {
            GameMessage.Ui.HighScoresUpdated.AddObserver<List<GameStats.ScoreEntry>>(OnHighScoresUpdated);
            m_ExitButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            GameMessage.Ui.HighScoresUpdated.RemoveObserver<List<GameStats.ScoreEntry>>(OnHighScoresUpdated);
        }

        void OnHighScoresUpdated (List<GameStats.ScoreEntry> scoreEntries)
        {
            m_NoScoresLabel.SetActive(scoreEntries.Count == 0);
            m_ScrollRect.gameObject.SetActive(scoreEntries.Count != 0);

            m_StringBuilder.Clear();

            for (int i = 0; i < scoreEntries.Count; i++)
                m_StringBuilder.AppendLine($"{scoreEntries[i].score}   |-   {scoreEntries[i].date}");

            m_HighScoresText.text = m_StringBuilder.ToString();
            // reset scroll position
            m_ScrollRect.verticalNormalizedPosition = 0;
        }

        public void Show()
        {
            m_Panel.SetActive(true);
        }

        public void Hide()
        {
            m_Panel.SetActive(false);
        }
    }
}