using System;
using System.Collections.Generic;
using System.Linq;

namespace AGames.SpaceInvaders
{
    [System.Serializable]
    public class GameStats
    {
        #region Game Stats To Store
        public List<ScoreEntry> highScores;
        #endregion

        public GameStats ()
        {
            highScores = new List<ScoreEntry>();
        }

        public void AddScore(int score, int maxCount)
        {
            highScores.Add(new ScoreEntry(score));
            highScores = highScores.OrderByDescending(x => x.score).ToList();

            // remove last element 
            while(highScores.Count > maxCount)
            {
                highScores.RemoveAt(highScores.Count-1);
            }
        }

        [System.Serializable]
        public class ScoreEntry
        {
            public ScoreEntry(int score)
            {
                this.score = score;
                this.date = DateTime.Today.ToString("d");
            }

            public int score;
            public string date;
        }
    }
}