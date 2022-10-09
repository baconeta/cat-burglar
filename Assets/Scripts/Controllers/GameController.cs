using Tasks;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private ControllerManager _cm;

        public bool debugMode;
        public bool testGameMode;

        private void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();

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
            TaskBase newTask = ScriptableObject.CreateInstance<TaskBase>();
            newTask.taskText = "Collect an apple";
            newTask.taskType = TaskController.TaskType.CollectItem;
            _cm.TaskController.AddTask(newTask);
        }
    }
}