using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


// TODO make all controllers singletons?
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

        [Tooltip("A controller that handles the administration of tasks for the player to complete")] [SerializeField]
        private TaskController taskController;

        [Tooltip("A controller that handles the dynamic HUD elements")] [SerializeField]
        private HUDController hudController;

        [Tooltip("A controller that handles the inventory for the player")] [SerializeField]
        private InventoryManager inventoryManager;

        [Tooltip("A controller that handles the player achievement system")] [SerializeField]
        private AchievementController achievementController;

        [Tooltip("A controller that handles the end game screen")] [SerializeField]
        private EndGameScreenController endGameScreenController;

        public GameController GameController => gameController;
        public TaskController TaskController => taskController;
        public HUDController HUDController => hudController;
        public InventoryManager InventoryManager => inventoryManager;
        public AchievementController Achievements => achievementController;
        public EndGameScreenController EndGameScreenController => endGameScreenController;

        private void Start()
        {
            if (!gameController) gameController = FindObjectOfType<GameController>();
            if (!taskController) taskController = FindObjectOfType<TaskController>();
            if (!hudController) hudController = FindObjectOfType<HUDController>();
            if (!inventoryManager) inventoryManager = FindObjectOfType<InventoryManager>();
            if (!achievementController) achievementController = FindObjectOfType<AchievementController>();
            if (!endGameScreenController) endGameScreenController = FindObjectOfType<EndGameScreenController>();
        }
    }
}