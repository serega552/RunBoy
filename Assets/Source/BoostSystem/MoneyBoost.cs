using System;
using Tasks;
using Tasks.SO;
using YG;

namespace BoostSystem
{
    public class MoneyBoost : Boost
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
            YandexGame.savesData.CountMoneyBoost = Count;
            YandexGame.savesData.CountUpgradeMoneyBoost = CountUpgrade;
            YandexGame.SaveProgress();
        }

        public override void Load()
        {
            Count = YandexGame.savesData.CountMoneyBoost;
            CountUpgrade = YandexGame.savesData.CountUpgradeMoneyBoost;
            LoadTimer();
            Invoke(nameof(UpdateText), 0.3f);
        }

        private void GiveRewardBoost(string name, int amount)
        {
            if (name == Convert.ToString(ResourceType.MoneyBoost))
            {
                for (int i = 0; i < amount; i++)
                {
                    Increase();
                }
            }
        }
    }
}