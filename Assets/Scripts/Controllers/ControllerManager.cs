using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// The ControllerManager will be a one stop shop for all managers in the game. Any class that needs access to any
    /// managers should reference this class only and use the getters provided for each controller. This simplifies
    /// code clutter and makes it easy to define and make changes to behaviour or controller functionality
    /// </summary>
    public class ControllerManager : MonoBehaviour
    {
        [Tooltip("A controller for managing base setup and current state of the game.")] [SerializeField]
        private GameController gameController;

        public GameController GameController => gameController;

        private void Start()
        {
            if (!gameController) gameController = FindObjectOfType<GameController>();
        }
    }
}