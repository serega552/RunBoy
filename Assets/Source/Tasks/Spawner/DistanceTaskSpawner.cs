using YG;

public class DistanceTaskSpawner : TaskSpawner
{
    public override void Save()
    {
        if (YandexGame.savesData.AmountDistanceProgreses.Count < ActiveTasks.Count)
        {
            YandexGame.savesData.AmountDistanceProgreses.Add(0);
            YandexGame.SaveProgress();
        }
    }

    public override void Load()
    {
        _amountProgreses = YandexGame.savesData.AmountDistanceProgreses;
        base.Load();
    }
}
