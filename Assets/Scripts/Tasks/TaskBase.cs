using Controllers;

namespace Tasks
{
    public struct TaskBase
    {
        public string TaskText;
        public TaskController.TaskType TaskType;
        public bool Completed;

        public TaskBase(string taskText, TaskController.TaskType taskType)
        {
            TaskText = taskText;
            TaskType = taskType;
            Completed = false;
        }
    }
}