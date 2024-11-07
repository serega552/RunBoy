using System;
using YG;

public class SpeedBoost : Boost
{
    private void OnEnable()
    {
        AwardGiver.OnReward += GiveRewardBoost;
    }

    private void OnDisable()
    {
        AwardGiver.OnReward -= GiveRewardBoost;
    }

    public override void Save()
    {
        YandexGame.savesData.CountSpeedBoost = Count;
        YandexGame.savesData.CountUpgradeSpeedBoost = CountUpgrade;
        YandexGame.SaveProgress();
    }

    public override void Load()
    {
        Count = YandexGame.savesData.CountSpeedBoost;
        CountUpgrade = YandexGame.savesData.CountUpgradeSpeedBoost;
        LoadTimer();
        Invoke("UpdateText", 0.3f);
    }

    private void GiveRewardBoost(string name, int amount)
    {
        if (name == Convert.ToString(ResourceType.SpeedBoost))
        {
            for(int i = 0; i <= amount; i++)
            {
                Increase();
            }
        }
    }
}
