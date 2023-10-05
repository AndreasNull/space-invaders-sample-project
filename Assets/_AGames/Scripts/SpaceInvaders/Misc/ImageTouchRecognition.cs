using UnityEngine;
using UnityEngine.EventSystems;

namespace AGames.SpaceInvaders
{
    [RequireComponent(typeof(UnityEngine.UI.Graphic))]
    public class ImageTouchRecognition : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // the values must be the same as the game messages values respectively
        public enum InputType { Left = 50, Right = 51, Fire = 52}

        #region Inspector
        [SerializeField] InputType m_Type;
        #endregion

        private bool m_IsPressed = false;

        public void OnPointerDown(PointerEventData eventData) => m_IsPressed = true;

        public void OnPointerUp(PointerEventData eventData) => m_IsPressed = false;

        void Update() => ((int)m_Type).Post(m_IsPressed);
    }
}