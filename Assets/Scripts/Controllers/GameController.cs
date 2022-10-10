using Inventory.Items;
using Tasks;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// This class should handle and deal with game loop logic - pause, game progression, etc
    /// </summary>
    public class GameController : MonoBehaviour
    {
        private ControllerManager _cm;

        public bool debugMode;
        public bool testGameMode;

        private void Awake()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        private void Start()
        {
            GameWindowManagement();

            // Handy to use when building manual game logic 
            if (testGameMode)
            {
                TestGameMode();
            }
        }

        private static void GameWindowManagement()
        {
            Cursor.lockState = CursorLockMode.Locked; // has an issue with mouse delta spikes in frame 1
            Cursor.visible = false;
        }

        private void TestGameMode()
        {
            TaskBase newTask = new("Collect an apple", TaskController.TaskType.CollectItem, typeof(Apple));
            _cm.TaskController.AddTask(newTask);
        }
    }
}