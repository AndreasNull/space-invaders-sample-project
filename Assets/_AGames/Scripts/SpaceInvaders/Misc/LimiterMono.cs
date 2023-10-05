using UnityEngine;

namespace AGames.SpaceInvaders
{
    [RequireComponent(typeof(BoxCollider))]
    public class LimiterMono : MonoBehaviour
    {
        protected virtual void Awake ()
        {
            // enforce collider as a trigger
            GetComponent<BoxCollider>().isTrigger = true;
        }

#if UNITY_EDITOR
        BoxCollider m_Collider;

        private void OnDrawGizmos()
        {
            if (m_Collider == null)
                m_Collider = GetComponent<BoxCollider>();

            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_Collider.center, m_Collider.size);
        }
#endif
    }

}
