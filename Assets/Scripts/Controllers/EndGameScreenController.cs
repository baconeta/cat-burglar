using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class EndGameScreenController : MonoBehaviour
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene("TitleScene");
        }
        
        public void Replay()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
