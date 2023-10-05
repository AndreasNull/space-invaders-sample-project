using System;
using UnityEngine;

namespace AGames.SpaceInvaders
{
    public class EnemyWaveSpawnHandler
    {
        // dependencies
        readonly EnemyWave m_EnemyWave;
        readonly Settings m_Settings;
        readonly EnemyMono.Factory m_EnemyFactory;
        readonly EnemyWaveRegistry m_Registry;

        public int waveCount { get; set; }

        public event Action onNextWave;

        // constructor
        public EnemyWaveSpawnHandler(EnemyWave enemyWave, Settings settings, EnemyMono.Factory factory, EnemyWaveRegistry registry)
        {
            m_EnemyWave = enemyWave;
            m_Settings = settings;
            m_EnemyFactory = factory;
            m_Registry = registry;
        }

        public void ResetWaves()
        {
            //reset waves counter
            waveCount = 0;
            // post EnemyWavesUpdated message
            GameMessage.Ui.EnemyWavesUpdated.Post<int>(waveCount);
        }

        public void NextWave()
        {
            // increase waves counter
            waveCount++;
            // post EnemyWavesUpdated message
            GameMessage.Ui.EnemyWavesUpdated.Post<int>(waveCount);
            m_Registry.OnNextWave(m_Settings.enemyCount, m_Settings.enemyPerRow);
            SpawnEnemies();
            // call event
            onNextWave?.Invoke();
        }

        public void SpawnEnemies()
        {
            float verticalOffset = 0;
            float horizontalOffset = ((m_Settings.enemyPerRow - 1) * m_Settings.enemyDistance) * -0.5f;

            for (int i = 0; i < m_Settings.rows; i++)
            {
                SpawnRow(m_EnemyWave.position + Vector3.forward * verticalOffset, i, horizontalOffset);
                verticalOffset += m_Settings.rowDistance;
            }
        }

        private void SpawnRow(Vector3 initPosition, int row, float horizontalOffset)
        {
            float localOffset = 0;

            for (int i = 0; i < m_Settings.enemyPerRow; i++)
            {
                // create enemy
                int id = (row * m_Settings.enemyPerRow) + i;
                var enemy = m_EnemyFactory.Create(m_Settings.GetEnemyConfig(row), id);
                // set enemy position
                enemy.transform.position = initPosition + Vector3.right * (localOffset + horizontalOffset);
                localOffset += m_Settings.enemyDistance;
                // register enemy
                m_Registry.RegisterEnemy(enemy);
            }
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] int m_Rows;
            [SerializeField] int m_EnemyPerRow;
            [SerializeField] float m_RowDistance;
            [SerializeField] float m_EnemyDistance;

            [Header("Enemy type per row")]
            [SerializeField] EnemyConfig[] m_EnemyConfig;

            public int rows => m_Rows;
            public float rowDistance => m_RowDistance;
            public int enemyPerRow => m_EnemyPerRow;
            public int enemyCount => m_EnemyPerRow * m_Rows;
            public float enemyDistance => m_EnemyDistance;
            public EnemyConfig GetEnemyConfig(int index) => m_EnemyConfig[index];
        }
    }
}
