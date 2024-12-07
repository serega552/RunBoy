using System;
using Tasks.SO;
using YG;

namespace BoostSystem
{
    public class EnergyBoost : Boost
    {
        public override void Save()
        {
            YandexGame.savesData.CountEnergyBoost = Count;
            YandexGame.savesData.CountUpgradeEnergyBoost = CountUpgrade;
            YandexGame.SaveProgress();
        }

        public override void Load()
        {
            Count = YandexGame.savesData.CountEnergyBoost;
            CountUpgrade = YandexGame.savesData.CountUpgradeEnergyBoost;
            base.Load();
        }

        public override void AddRewardBoost(string name, int amount)
        {
            if (name == Convert.ToString(ResourceType.EnergyBoost))
            {
                base.AddRewardBoost(name, amount);
            }
        }
    }
}