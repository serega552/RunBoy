using System;
using Tasks.SO;
using YG;

namespace BoostSystem
{
    public class MoneyBoost : Boost
    {
        public override void Save()
        {
            YandexGame.savesData.CountMoneyBoost = Count;
            YandexGame.savesData.CountUpgradeMoneyBoost = CountUpgrade;
            YandexGame.SaveProgress();
        }

        public override void Load()
        {
            Count = YandexGame.savesData.CountMoneyBoost;
            CountUpgrade = YandexGame.savesData.CountUpgradeMoneyBoost;
            base.Load();
        }

        public override void AddRewardBoost(string name, int amount)
        {
            if (name == Convert.ToString(ResourceType.MoneyBoost))
            {
                base.AddRewardBoost(name, amount);
            }
        }
    }
}