using TimeInspector;
using UnityEngine;
using YG;

namespace Initers
{
    public class TaskTimeLoader : MonoBehaviour
    {
        [SerializeField] private TaskTimeInspector _taskInspector;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
            {
                _taskInspector.Load();
                _taskInspector.RefreshTime();
            }
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += _taskInspector.Load;
            YandexGame.GetDataEvent += _taskInspector.RefreshTime;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= _taskInspector.Load;
            YandexGame.GetDataEvent -= _taskInspector.RefreshTime;
        }
    }
}