using Controllers;

namespace Tasks
{
    public class MeowTask : TaskBase
    {
        private int _numMeowsDone = 0;

        public MeowTask(string task, int meows)
        {
            taskText = task;
            num = meows;
            taskType = TaskController.TaskType.Meow;
        }

        public override void PrepareTaskObjects()
        {
            taskType = TaskController.TaskType.Meow;
        }

        public bool AddMeow()
        {
            _numMeowsDone += 1;

            if (_numMeowsDone >= num)
            {
                completed = true;
                return true;
            }

            return false;
        }
    }
}