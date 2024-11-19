using System.Collections.Generic;
using TimeInspector;
using Tasks.SO;
using UnityEngine;
using Tasks.Factory;

namespace Tasks.Spawner
{
    public abstract class TaskSpawner : MonoBehaviour
    {
        private readonly List<TaskView> _activeTasks = new List<TaskView>();
        private TaskFactory _taskFactory;

        [SerializeField] private GameObject _prefabTask;
        [SerializeField] private Transform _contentTasks;
        [SerializeField] private List<Task> _tasks = new List<Task>();
        [SerializeField] private TaskTimeInspector _timeInspector;

        protected List<float> AmountProgreses = new List<float>();

        public List<TaskView> ActiveTasks => _activeTasks;
        public TaskTimeInspector TaskInspector => _timeInspector;

        private void Awake()
        {
            TaskCounter.Init();

            _taskFactory = new TaskFactory(_prefabTask, _contentTasks);

            SpawnTasks();
        }

        private void OnDisable()
        {
            foreach (var task in _activeTasks)
            {
                task.OnComplete -= DestroyTask;
            }
        }

        public void SpawnTasks()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                TaskView taskView = _taskFactory.CreateTask(_tasks[i], i);
                _activeTasks.Add(taskView);
            }

            foreach (var task in _activeTasks)
            {
                task.OnComplete += DestroyTask;
            }

            Save();
            Load();
        }

        public virtual void RefreshTasks()
        {
            foreach (var task in _activeTasks)
            {
                Destroy(task.gameObject);
            }

            _activeTasks.Clear();

            SpawnTasks();
        }

        public abstract void Save();

        public virtual void Load()
        {
            for (int i = 0; i < _activeTasks.Count; i++)
            {
                _activeTasks[i].InitProgress(AmountProgreses[i]);
            }

            for (int i = 0; i < _activeTasks.Count; i++)
            {
                if (AmountProgreses[i] == -1)
                {
                    _activeTasks[i].gameObject.SetActive(false);
                }
            }
        }

        private void DestroyTask(TaskView taskView)
        {
            taskView.gameObject.SetActive(false);
        }
    }
}
