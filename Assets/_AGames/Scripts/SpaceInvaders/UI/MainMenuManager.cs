using UnityEngine;
using UnityEngine.UI;

namespace AGames.SpaceInvaders.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Main Menu")]
        [SerializeField] GameObject m_Panel;
        [SerializeField] Button m_StartGameButton;
        [SerializeField] Button m_HighScoresButton;

        [Header("Sub Panels")]
        [SerializeField] HighScoresPanelManager m_HighScoresPanel;

        private void Awake()
        {
            m_StartGameButton.onClick.AddListener(OnStartGameButton);
            m_HighScoresButton.onClick.AddListener(m_HighScoresPanel.Show);

            GameMessage.Ui.ShowMainMenu.AddObserver(ShowPanel);
        }

        private void OnDestroy()
        {
            GameMessage.Ui.ShowMainMenu.RemoveObserver(ShowPanel);
        }

        private void OnStartGameButton()
        {
            // set button interactable to false, to prevent multiple clickes/touches to the button
            m_StartGameButton.interactable = false;
            GameMessage.StateCommand.Post(GameStateCommand.StartGameSession);
            HidePanel();
        }

        private void ShowPanel()
        {
            m_StartGameButton.interactable = true;
            m_HighScoresButton.interactable = true;
            // hide subpanrls
            m_HighScoresPanel.Hide();
            m_Panel.SetActive(true);
        }
        private void HidePanel() => m_Panel.SetActive(false);
    }
}