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
        private bool _gameRunning;
        [Range(1, 8)] public int tasksPerRound;
        [Range(1, 20)] public int roundsToComplete = 1;

        [Header("Debugging")] [Tooltip("Enable console logging for game loop logic.")]
        public bool debugMode;

        [Tooltip("Enables simple preconfigured game loop logic.")]
        public bool testGameMode;

        public AudioSource winSound;

        private void Awake()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        private void Start()
        {
            CursorManagement(false);

            _gameRunning = true;

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

        private static void CursorManagement(bool showCursor)
        {
            Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = showCursor;
        }

        public bool GameRunning()
        {
            return _gameRunning;
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
            if (_cm.GameController.debugMode) Debug.Log("Start round " + _roundsCompleted + 1);

            // Set up a round with a series of tasks - possibly we could design all possible task types in the editor or separately
            for (var i = 0; i < tasksPerRound; i++)
            {
                // Create and add a task to the list
                var task = Random.Range(0, _cm.TaskController.possibleTasks.Count);
                _cm.TaskController.AddTask(_cm.TaskController.possibleTasks[task]);
            }
        }

        public void EndRound()
        {
            // This should handle some scoring and add it to the total score, increase the rounds completed
            // Once we have done all the rounds we should call EndGame()
            _roundsCompleted += 1;
            if (_cm.GameController.debugMode) Debug.Log("Completed round " + _roundsCompleted);

            if (roundsToComplete > _roundsCompleted)
            {
                StartRound();
            }
            else
            {
                EndGame(true);
            }
        }

        public void Caught()
        {
            _cm.Achievements.GetCaught();
            EndGame(false);
        }

        private void EndGame(bool win)
        {
            if(win){
                winSound.Play();
            }

            var gameEndMessage = "Game " + (win ? "won!" : "over!");

            if (_cm.GameController.debugMode) Debug.Log(gameEndMessage);

            // We may have to flesh out a bunch of logic to use a var like bool gameRunning so we can prevent input,
            // and ai actions once the game stops
            _gameRunning = false;

            // Here we would show the end game screen and let the user select play again/go to menu/ etc
            _cm.HUDController.ShowEndGameScreen(gameEndMessage);
            CursorManagement(true);

            // We could also very easily have local high scores here (simple to implement)
        }
    }
}