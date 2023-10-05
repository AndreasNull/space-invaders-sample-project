using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AGames.SpaceInvaders
{
    /// <summary>
    /// Enemy facade class
    /// </summary>
    public class EnemyWaveMono : MonoBehaviour
    {
        // dependences
        EnemyWaveSpawnHandler m_WaveSpawnHanlder;
        EnemyWaveRegistry m_WaveRegistry;

        [Inject]
        public void Construct(EnemyWaveSpawnHandler waveSpawnHandler, EnemyWaveRegistry waveRegistry)
        {
            m_WaveSpawnHanlder = waveSpawnHandler;
            m_WaveRegistry = waveRegistry;
        }

        public void ResetWaves() => m_WaveSpawnHanlder.ResetWaves();
        public void NextWave() => m_WaveSpawnHanlder.NextWave();
        public void Clear() => m_WaveRegistry.Clear();
        public int score => m_WaveRegistry.score;
        public int waves => m_WaveSpawnHanlder.waveCount;
    }
}