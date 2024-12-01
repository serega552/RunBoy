using System;
using TMPro;
using UnityEngine;
using YG;

namespace TimeInspector
{
    public class TaskTimeInspector : MonoBehaviour
    {
        private readonly int _dailyTime = 1;
        private readonly int _weeklyTime = 3;

        [SerializeField] private TMP_Text _weeklyTimerText;
        [SerializeField] private TMP_Text _dailyTimerText;

        private int _startWeeklyTime;
        private int _startDailyTime;

        public event Action DailyTimeGoned;

        public event Action WeeklyTimeGoned;

        public void Load()
        {
            _startDailyTime = YandexGame.savesData.StartDailyTime;
            _startWeeklyTime = YandexGame.savesData.StartWeeklyTime;
        }

        public void RefreshTime()
        {
            if (_startDailyTime == DateTime.Now.Day)
            {
                _dailyTimerText.text = _dailyTime.ToString();
            }
            else
            {
                _startDailyTime = DateTime.Now.Day;
                DailyTimeGoned?.Invoke();
                Save();
            }

            if (DateTime.Now.Day < _startWeeklyTime + _weeklyTime)
            {
                int subtractDays = DateTime.Now.Day - _startWeeklyTime;
                int daysLeft = _weeklyTime - subtractDays;

                if (daysLeft > _weeklyTime)
                {
                    daysLeft = _weeklyTime;
                    _startWeeklyTime = DateTime.Now.Day;
                    Save();
                }

                _weeklyTimerText.text = daysLeft.ToString();
            }
            else
            {
                _startWeeklyTime = DateTime.Now.Day;
                WeeklyTimeGoned?.Invoke();
                Save();
            }
        }

        private void Save()
        {
            YandexGame.savesData.StartDailyTime = _startDailyTime;
            YandexGame.savesData.StartWeeklyTime = _startWeeklyTime;
            YandexGame.SaveProgress();
        }
    }
}