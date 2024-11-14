using System.Collections.Generic;
using Tasks.SO;
using TimeInspector;
using UnityEngine;

namespace Tasks.Spawner
{
    public abstract class TaskSpawner : MonoBehaviour
    {
        private readonly List<TaskView> _activeTasks = new List<TaskView>();
        private readonly Dictionary<int, float> _activeDailyId = new Dictionary<int, float>();

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
                GameObject gameObject = Instantiate(_prefabTask, _contentTasks, false);
                TaskView taskView = gameObject.GetComponent<TaskView>();
                taskView.transform.SetParent(_contentTasks);
                taskView.GetTask(_tasks[i]);
                taskView.Init();
                taskView.InitId(i);
                taskView.gameObject.SetActive(true);
                _activeTasks.Add(taskView);
                Save();
            }

            foreach (var task in _activeTasks)
            {
                task.OnComplete += DestroyTask;
            }

            Load();
        }

        public virtual void RefreshTasks()
        {
            _activeDailyId.Clear();

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