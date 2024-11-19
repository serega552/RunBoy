using System;
using Tasks;
using Tasks.SO;
using YG;

namespace BoostSystem
{
    public class EnergyBoost : Boost
    {
        private void OnEnable()
        {
            AwardGiver.Rewarding += GiveRewardBoost;
        }

        private void OnDisable()
        {
            AwardGiver.Rewarding -= GiveRewardBoost;
        }

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

            LoadTimer();
            Invoke(nameof(UpdateText), 0.3f);
        }

        private void GiveRewardBoost(string name, int amount)
        {
            if (name == Convert.ToString(ResourceType.EnergyBoost))
            {
                for (int i = 0; i <= amount; i++)
                {
                    Increase();
                }
            }
        }
    }
}