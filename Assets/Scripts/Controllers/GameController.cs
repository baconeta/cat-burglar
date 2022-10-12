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

        [Header("Game Loop Logic")] private int _tasksCompleted;
        private int _roundsCompleted;
        public int tasksPerRound;
        public int roundsToComplete;

        [Header("Debugging")] public bool debugMode;
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
            else
            {
                StartRound();
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

            TaskBase newTask2 = new("Retrieve a halloween pumpkin.", TaskController.TaskType.ReturnItem,
                typeof(Pumpkin));
            _cm.TaskController.AddTask(newTask2);
        }

        private void StartRound()
        {
            // Set up a round with a series of tasks - possibly we could design all possible task types in the editor or separately
            for (int i = 0; i < tasksPerRound; i++)
            {
                // Create and add a task to the list
                int task = Random.Range(0, _cm.TaskController.possibleTasks.Count);
                _cm.TaskController.AddTask(_cm.TaskController.possibleTasks[task]);
            }
        }

        public void EndRound()
        {
            // This should handle some scoring and add it to the total score, increase the rounds completed
            // Once we have done all the rounds we should call EndGame()
        }

        public void EndGame()
        {
            // We may have to flesh out a bunch of logic to use a var like bool gameRunning so we can prevent input,
            // and ai actions once the game stops
            // Here we would show the end game screen and let the user select play again/go to menu/ etc
            // We could also very easily have local high scores here (simple to implement)
        }
    }
}