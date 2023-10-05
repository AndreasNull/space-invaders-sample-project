using System;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class EnemyWave 
    {
        // dependences
        readonly Transform m_Transform;

        public EnemyWave(Transform transform)
        {
            m_Transform = transform;
        }

        public Vector3 position
        {
            get => m_Transform.position;
            set => m_Transform.position = value;
        }

        public void InitPosition() => m_Transform.position = Vector3.zero;
        public void Move(Vector3 offset) => m_Transform.position += offset;

    }
}