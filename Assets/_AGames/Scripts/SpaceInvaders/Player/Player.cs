using UnityEngine;

namespace AGames.SpaceInvaders
{
    public class Player
    {
        // dependences
        readonly Camera m_Camera;
        readonly Transform m_Transform;
        readonly GameObject m_InvulnerableFX;

        // constructor
        public Player(Camera camera, Transform transform, GameObject invulnerableFX)
        {
            m_Camera = camera;
            m_Transform = transform;
            m_InvulnerableFX = invulnerableFX;
        }

        public void EnableInvunerableFX() => m_InvulnerableFX.SetActive(true);
        public void DisableInvunerableFX() => m_InvulnerableFX.SetActive(false);

        public bool isLocked { get; set; }

        public Vector3 position 
        { 
            get => m_Transform.position; 
            set => m_Transform.position = value; 
        }

        public Vector3 forward => m_Transform.forward;

        public Quaternion rotation => m_Transform.rotation;

        public void Move(Vector3 offset)
        {
            // this will be the new position
            Vector3 newPosition = m_Transform.position + offset;
            // map world new position to cameras viewport position [0,1]
            Vector3 viewposrtPosition = m_Camera.WorldToViewportPoint(newPosition);

            // check if we are out of screen
            if (viewposrtPosition.x < 0 || viewposrtPosition.x > 1)
                newPosition = m_Transform.position;

            // apply movement
            m_Transform.position = newPosition;
        }
    }
}