using System;
using System.Collections.Generic;
using Tasks.SO;

namespace Tasks
{
    public static class TaskCounter
    {
        public static event Action<float, string> Exicuted;

        private static Dictionary<string, bool> _tasks = new Dictionary<string, bool>();
        private static string[] _tasksArray = Enum.GetNames(typeof(TaskType));

        public static void Init()
        {
            for (int i = 0; i < _tasksArray.Length; i++)
            {
                if (_tasks.ContainsKey(_tasksArray[i]))
                    _tasks.Add(_tasksArray[i], false);
            }
        }

        public static void StartTask(string name)
        {
            if (_tasks.ContainsKey(name))
                _tasks[name] = true;
        }

        public static void CompleteTask(string name)
        {
            if (_tasks.ContainsKey(name))
                _tasks[name] = false;
        }

        public static void IncereaseProgress(int amount, string name)
        {
            Exicuted?.Invoke(amount, name);
        }
    }
}