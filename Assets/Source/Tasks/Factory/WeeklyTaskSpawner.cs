using YG;

namespace Tasks.Spawner
{
    public class WeeklyTaskSpawner : TaskSpawner
    {
        private void OnEnable()
        {
            TaskInspector.WeeklyTimeGoned += RefreshTasks;
        }

        private void OnDisable()
        {
            TaskInspector.WeeklyTimeGoned -= RefreshTasks;
        }

        public override void Save()
        {
            if (YandexGame.savesData.AmountWeeklyProgreses.Count < ActiveTasks.Count)
            {
                YandexGame.savesData.AmountWeeklyProgreses.Add(0);
                YandexGame.SaveProgress();
            }
        }

        public override void RefreshTasks()
        {
            YandexGame.savesData.AmountWeeklyProgreses.Clear();
            base.RefreshTasks();
        }

        public override void Load()
        {
            _amountProgreses = YandexGame.savesData.AmountWeeklyProgreses;
            base.Load();
        }
    }
}