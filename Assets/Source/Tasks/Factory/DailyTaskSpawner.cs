using YG;

namespace Tasks.Spawner
{
    public class DailyTaskSpawner : TaskSpawner
    {
        private void OnEnable()
        {
            TaskInspector.DailyTimeGoned += RefreshTasks;
        }

        private void OnDisable()
        {
            TaskInspector.DailyTimeGoned -= RefreshTasks;
        }

        public override void Save()
        {
            if (YandexGame.savesData.AmountDailyProgreses.Count < ActiveTasks.Count)
            {
                YandexGame.savesData.AmountDailyProgreses.Add(0);
                YandexGame.SaveProgress();
            }
        }

        public override void RefreshTasks()
        {
            YandexGame.savesData.AmountDailyProgreses.Clear();
            base.RefreshTasks();
        }

        public override void Load()
        {
            _amountProgreses = YandexGame.savesData.AmountDailyProgreses;
            base.Load();
        }
    }
}