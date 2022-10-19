using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class ScoreController : MonoBehaviour
    {
        private List<int> HighScores { get; set; }
        public int LatestScore { get; private set; }
        [SerializeField] private int scoresToKeep = 5;

        private void Start()
        {
            // Add five zeros to start with.
            HighScores = Enumerable.Repeat(0, scoresToKeep).ToList();
            for (var i = 0; i < scoresToKeep; i++)
            {
                HighScores.Add(PlayerPrefs.GetInt("Score" + (i + 1)));
            }

            HighScores.Sort();
            HighScores.Reverse();

            // Only keep the best scores.
            HighScores = HighScores.GetRange(0, scoresToKeep);
        }

        public List<int> GetScores()
        {
            return HighScores;
        }

        public void AddScore(int score)
        {
            LatestScore = score;
            HighScores.Add(score);
            // Sort by largest-first.
            HighScores.Sort();
            HighScores.Reverse();
            // Only keep the X best times.
            HighScores = HighScores.GetRange(0, 5);
            SaveBestScores();
        }

        private void SaveBestScores()
        {
            for (var i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("Score" + (i + 1), HighScores[i]);
            }
        }
    }
}