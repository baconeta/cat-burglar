using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class IntroScene : MonoBehaviour
    {
        public float loadingTime = 0.5f;

        private void Start()
        {
            Invoke($"LoadTitleScreen", loadingTime);
        }

        private void LoadTitleScreen()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}