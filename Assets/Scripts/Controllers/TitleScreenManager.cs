using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}