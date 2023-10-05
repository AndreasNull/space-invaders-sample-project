using UnityEngine;
using System.IO;

namespace AGames.SpaceInvaders
{
    public class GameStatsHandler
    {
        // dependeces
        readonly Settings m_Settings;

        GameStats m_GameStats;
        readonly string m_FullPath;

        public GameStatsHandler(Settings settings)
        {
            m_Settings = settings;
            m_FullPath = settings.fullSavePath;
        }

        public void Restore()
        {
            if (!File.Exists(m_FullPath))
            {
                m_GameStats = new GameStats();
                return;
            }
            else
            {

                string json = File.ReadAllText(m_Settings.fullSavePath);
                m_GameStats = JsonUtility.FromJson<GameStats>(json);
            }
        }

        public void BroadcastHighScore() => GameMessage.Ui.HighScoresUpdated.Post(m_GameStats.highScores);

        public void AddScore(int score)
        {
            m_GameStats.AddScore(score, m_Settings.totalHighScoresCount);
            Store();
        }

        private void Store()
        {
            string json = JsonUtility.ToJson(m_GameStats);
            Debug.Log(json);
            File.WriteAllText(m_FullPath, json);
            Debug.Log($"save data @{m_FullPath}");
        }

        [System.Serializable]
        public class Settings
        {
            [SerializeField] string m_SavePath;
            [SerializeField] int m_TotalHighScoresCount;

            public string fullSavePath => Path.Combine(Application.persistentDataPath, m_SavePath);
            public int totalHighScoresCount => m_TotalHighScoresCount;
        }



    }
}