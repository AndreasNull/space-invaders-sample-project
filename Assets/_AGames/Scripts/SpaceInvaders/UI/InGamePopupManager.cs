using UnityEngine;

namespace AGames.SpaceInvaders.UI
{
    public class InGamePopupManager : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI m_MessageTMPro;
        [SerializeField] Animator m_Animator;

        readonly int POP_PARAM_ID = Animator.StringToHash("pop");

        private void Awake()
        {
            GameMessage.Ui.InGamePopup.AddObserver<string>(Pop);
        }

        private void OnDestroy()
        {
            GameMessage.Ui.InGamePopup.RemoveObserver<string>(Pop);
        }

        private void Pop(string message)
        {
            m_MessageTMPro.text = message;
            m_Animator.SetTrigger(POP_PARAM_ID);
        }
    }
}