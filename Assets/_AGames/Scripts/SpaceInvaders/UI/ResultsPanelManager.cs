using UnityEngine;
using UnityEngine.UI;

namespace AGames.SpaceInvaders.UI
{
    public class ResultsPanelManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField] GameObject m_Panel;
        [SerializeField] Button m_ExitButton;
        [SerializeField] TMPro.TextMeshProUGUI m_ScoreTMPro;
        [SerializeField] TMPro.TextMeshProUGUI m_WaveTMPro;
        #endregion

        private void Awake()
        {
            GameMessage.Ui.ShowResults.AddObserver<int,int>(Show);
            GameMessage.Ui.HideResults.AddObserver(Hide);

            m_ExitButton.onClick.AddListener(() => {
                GameMessage.StateCommand.Post(GameStateCommand.GoToMainMenu);
                Hide();
                });
        }

        private void OnDestroy()
        {
            GameMessage.Ui.ShowResults.RemoveObserver<int, int>(Show);
            GameMessage.Ui.HideResults.RemoveObserver(Hide);
        }

        public void Show(int score, int waves)
        {
            m_ScoreTMPro.text = score.ToString();
            m_WaveTMPro.text = waves.ToString();
            m_Panel.SetActive(true);

        }

        public void Hide()
        {
            m_Panel.SetActive(false);
        }
    }
}