using UnityEngine;

namespace AGames.SpaceInvaders
{
    public class EnemyWaveLimiterMono : LimiterMono
    {
        public enum Type { Left, Right }

        #region Inspector
        [SerializeField] Type m_Type;
        [SerializeField] Camera m_Camera;
        #endregion

        private bool m_Triggered = false;

        protected override void Awake()
        {
            base.Awake();

            GameMessage.Gameplay.EnemyLimit.AddObserver<Type>(OnEnemyLimit);

            // place limiters based in camera viewport
            switch (m_Type)
            {
                case Type.Left:
                    transform.position = new Vector3(m_Camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, transform.position.y, transform.position.z);
                    break;
                case Type.Right:
                    transform.position = new Vector3(m_Camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x, transform.position.y, transform.position.z);
                    break;
            }
        }

        private void OnDestroy()
        {
            GameMessage.Gameplay.EnemyLimit.RemoveObserver<Type>(OnEnemyLimit);
        }

        void OnEnemyLimit(Type type)
        {
            if (type != m_Type)
                m_Triggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_Triggered)
                return;

            if (other.GetComponent<EnemyMono>() != null)
            {
                m_Triggered = true;
                GameMessage.Gameplay.EnemyLimit.Post(m_Type);
                return;
            }
        }
    }
}