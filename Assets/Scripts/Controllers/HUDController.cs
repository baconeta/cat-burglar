using Tasks;
using TMPro;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Will handle the updating of HUD elements on the game screen, such as tasks, inventory, etc
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        private ControllerManager _cm;
        [SerializeField] private GameObject taskListHUD;
        [SerializeField] private GameObject newBlankTask;

        private void Awake()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void UpdateHUD()
        {
            // Update various semi-dynamic HUD elements when this function is called

            // Update tasks
            UpdateTasks();

            // Update inventory
            UpdateInventory();
        }

        private void UpdateInventory()
        {
            var items = _cm.InventoryManager.GetInventory();
            // update each inventory slot sprite on screen
        }

        private void UpdateTasks()
        {
            // Clear all HUD tasks first
            foreach (Transform child in taskListHUD.transform)
            {
                Destroy(child.gameObject);
            }

            // Refresh tasks back to HUD
            var tasks = _cm.TaskController.GetTasks();
            foreach (TaskBase task in tasks)
            {
                AddTaskToHUD(task);
            }
        }

        private void AddTaskToHUD(TaskBase task)
        {
            // Creates a text game object for displaying the text on screen
            GameObject taskTextObject = Instantiate(newBlankTask, taskListHUD.transform, true);
            TMP_Text taskText = taskTextObject.GetComponent<TMP_Text>();
            taskText.SetText(task.TaskText);
            if (task.Completed) MarkTaskDone(taskText);
        }

        // Add any change to text/font styles for completed tasks here
        private static void MarkTaskDone(TMP_Text taskText)
        {
            taskText.fontStyle = FontStyles.Strikethrough;
            taskText.color = new Color(0.1726f, 0.7178f, 0.2706f);
        }
    }
}