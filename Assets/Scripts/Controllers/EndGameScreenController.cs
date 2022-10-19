using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class EndGameScreenController : MonoBehaviour
    {
        [Header("High Scores")] 
        [SerializeField] private TMP_Text highscore1;
        [SerializeField] private TMP_Text highscore2;
        [SerializeField] private TMP_Text highscore3;
        [SerializeField] private TMP_Text highscore4;
        [SerializeField] private TMP_Text highscore5;
        [SerializeField] private Color normalTextColour;
        [SerializeField] private Color highlightedTextColour;

        private ControllerManager _cm;

        public void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene("TitleScene");
        }

        public void Replay()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void ShowHighScores()
        {
            var scores = _cm.ScoreController.GetScores();
            highscore1.text = scores[0].ToString();
            highscore2.text = scores[1].ToString();
            highscore3.text = scores[2].ToString();
            highscore4.text = scores[3].ToString();
            highscore5.text = scores[4].ToString();
            
            // Highlight the latest score for clarity
            var latest = _cm.ScoreController.LatestScore.ToString();
            highscore1.color = highscore1.text == latest
                ? highlightedTextColour
                : normalTextColour;
            highscore2.color = highscore2.text == latest
                ? highlightedTextColour
                : normalTextColour;
            highscore3.color = highscore3.text == latest
                ? highlightedTextColour
                : normalTextColour;
            highscore4.color = highscore4.text == latest
                ? highlightedTextColour
                : normalTextColour;
            highscore5.color = highscore5.text == latest
                ? highlightedTextColour
                : normalTextColour;
        }
    }
}