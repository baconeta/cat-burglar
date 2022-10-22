using Controllers;

namespace Tasks
{
    public class MechanicsTask : TaskBase
    {
        private int _numCompleted;

        public MechanicsTask(string task, int valueToGet, TaskController.TaskType type)
        {
            taskText = task;
            num = valueToGet;
            taskType = type;
            _numCompleted = 0;
        }

        public override void PrepareTaskObjects()
        {
            taskType = TaskController.TaskType.Meow;
        }

        public bool AddValue(int value)
        {
            _numCompleted += value;

            if (_numCompleted >= num)
            {
                completed = true;
                return true;
            }

            return false;
        }
    }
}