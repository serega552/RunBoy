using System;
using Tasks.SO;
using YG;

namespace BoostSystem
{
    public class SpeedBoost : Boost
    {
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
            base.Load();
        }

        public override void AddRewardBoost(string name, int amount)
        {
            if (name == Convert.ToString(ResourceType.SpeedBoost))
            {
                base.AddRewardBoost(name, amount);
            }
        }
    }
}