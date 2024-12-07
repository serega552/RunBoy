using Tasks.SO;
using UnityEngine;

namespace Tasks.Factory
{
    public class TaskFactory
    {
        private readonly GameObject _taskPrefab;
        private readonly Transform _parent;

        public TaskFactory(GameObject taskPrefab, Transform parent)
        {
            _taskPrefab = taskPrefab;
            _parent = parent;
        }

        public TaskView CreateTask(Task taskData, int id)
        {
            GameObject taskObject = Object.Instantiate(_taskPrefab, _parent);

            TaskView taskView = taskObject.GetComponent<TaskView>();

            taskView.AddTask(taskData);
            taskView.Init();
            taskView.InitId(id);

            taskView.gameObject.SetActive(true);

            return taskView;
        }
    }
}
