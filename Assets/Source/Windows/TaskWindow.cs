using UnityEngine;
using UnityEngine.UI;

public class TaskWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openDailyList;
    [SerializeField] private Button _openWeeklyList;
    [SerializeField] private Button _openDistanceList;
    [SerializeField] private DailyTaskWindow _dailyTaskList;
    [SerializeField] private WeeklyTaskWindow _weeklyTaskList;
    [SerializeField] private DistanceTaskWindow _distanceTaskList;

    private void Awake()
    {
        CloseWithoutSound();
        _dailyTaskList.OpenWithoutSound();
        _weeklyTaskList.CloseWithoutSound();
        _distanceTaskList.CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
        _openDailyList.onClick.AddListener(OpenDailyList);
        _openWeeklyList.onClick.AddListener(OpenWeeklyList);
        _openDistanceList.onClick.AddListener(OpenDistanceList);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
        _openDailyList.onClick.RemoveListener(OpenDailyList);
        _openWeeklyList.onClick.RemoveListener(OpenWeeklyList);
        _openDistanceList.onClick.RemoveListener(OpenDistanceList);
    }

    private void OpenDailyList()
    {
        _dailyTaskList.Open();
        _weeklyTaskList.CloseWithoutSound();
        _distanceTaskList.CloseWithoutSound();
    }

    private void OpenWeeklyList()
    {
        _dailyTaskList.CloseWithoutSound();
        _weeklyTaskList.Open();
        _distanceTaskList.CloseWithoutSound();
    }
    
    private void OpenDistanceList()
    {
        _dailyTaskList.CloseWithoutSound();
        _weeklyTaskList.CloseWithoutSound();
        _distanceTaskList.Open();
    }
}
