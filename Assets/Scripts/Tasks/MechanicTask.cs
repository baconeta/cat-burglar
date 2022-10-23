using Controllers;

namespace Tasks
{
    public class MechanicsTask : TaskBase
    {
        public MechanicsTask(string task, int valueToGet, TaskController.TaskType type)
        {
            taskText = task;
            num = valueToGet;
            taskType = type;
            numCompleted = 0;
        }

        public override void PrepareTaskObjects()
        {
            taskType = TaskController.TaskType.Meow;
        }

        public bool AddValue(int value)
        {
            numCompleted += value;

            if (numCompleted >= num)
            {
                completed = true;
                return true;
            }

            return false;
        }
    }
}