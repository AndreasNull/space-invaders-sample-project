using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    public class EnemyWaveRegistry : IInitializable, IDisposable
    {
        private List<EnemyMono> m_EnemyList;
        private List<int> m_BottomEnemyList;
        private int m_EnemyCount, m_EnemyPerRow;

        // constructor
        public EnemyWaveRegistry ()
        {
            m_EnemyList = new List<EnemyMono>();
            m_BottomEnemyList = new List<int>();
        }

        public int enemiesCount { get; private set; }
        public int score { get; private set; }

        public EnemyMono GetRandomBottomEnemy()
        {
            if (enemiesCount == 0)
                return null;

            int randomIndex = m_BottomEnemyList[UnityEngine.Random.Range(0, m_BottomEnemyList.Count)];
            return m_EnemyList[randomIndex];
        }

        #region Implement IInitializable
        public void Initialize()
        {
            GameMessage.Gameplay.EnemyKilled.AddObserver<EnemyMono>(OnEnemyKilled);
        }
        #endregion

        #region Implement IInitializable
        public void Dispose()
        {
            GameMessage.Gameplay.EnemyKilled.RemoveObserver<EnemyMono>(OnEnemyKilled);
        }
        #endregion

        public void RegisterEnemy(EnemyMono enemy)
        {
            m_EnemyList.Add(enemy);
        }

        private void OnEnemyKilled(EnemyMono enemy)
        {
            // increase score
            score += enemy.points;
            // post ui message
            GameMessage.Ui.PlayerScoreUpdated.Post(score);

            // decrease counter
            enemiesCount--;

            // replace bottom enemy id with the one above (if exist)
            m_BottomEnemyList.Remove(enemy.id);
            int nextEnemyId = enemy.id + m_EnemyPerRow;

            if (nextEnemyId < m_EnemyCount)
                m_BottomEnemyList.Add(nextEnemyId);

            // check if all enemies have been killed
            if (enemiesCount == 0)
            {
                //Debug.Log("All Enemies Killed");
                // post messagae
                GameMessage.Gameplay.AllEnemiesKilled.Post();
            }
        }

        public void Clear()
        {
            // clear enemy list
            for (int i = 0; i < m_EnemyList.Count; i++)
                if (m_EnemyList[i] != null)
                    m_EnemyList[i].Despawn();

            m_BottomEnemyList.Clear();
            m_EnemyList.Clear();

            score = 0;
            // post ui message
            GameMessage.Ui.PlayerScoreUpdated.Post(score);
            enemiesCount = 0;
        }

        public void OnNextWave(int enemyCount, int enemyPerRow)
        {
            m_EnemyCount = enemyCount;
            m_EnemyPerRow = enemyPerRow;

            // clear previous enemies
            m_EnemyList.Clear();
            // !IMPORTANT:: must be after Clear() method as Clear sets enemiesCount to 0
            // init enemiesCount
            this.enemiesCount = enemyCount;

            // init bottom enemy list ids
            m_BottomEnemyList.Clear();

            for (int i = 0; i < enemyPerRow; i++)
                m_BottomEnemyList.Add(i);
        }
    }
}