using BankSystem;
using IdNumbers;
using UI;
using UnityEngine;
using YG;

namespace Ads
{
    public class VideoAd : MonoBehaviour
    {
        private readonly int _countMoneyReward = 200;
        private readonly int _minRewardMultiply = 3;
        private readonly int _midleRewardMultiply = 5;
        private readonly int _maxRewardMultiply = 8;

        [SerializeField] private Bank _bank;
        [SerializeField] private PlayerResurrect _resurrect;
        [SerializeField] private BoostBuyButton _energyBoost;
        [SerializeField] private BoostBuyButton _speedBoost;
        [SerializeField] private BoostBuyButton _moneyBoost;
        [SerializeField] private AdRewarder _moneyReward;

        private void OnEnable()
        {
            _resurrect.Restarting += RefreshAdButtons;
            YandexGame.RewardVideoEvent += Rewarded;
        }

        private void OnDisable()
        {
            _resurrect.Restarting -= RefreshAdButtons;
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        private void Rewarded(int id)
        {
            switch (id)
            {
                case (int)Ids.One:
                    _bank.GiveMoney(_countMoneyReward);
                    break;
                case (int)Ids.Two:
                    _bank.GiveMoney(_countMoneyReward * _minRewardMultiply);
                    break;
                case (int)Ids.Three:
                    _bank.GiveMoney(_countMoneyReward * _midleRewardMultiply);
                    break;
                case (int)Ids.Four:
                    _bank.GiveMoney(_countMoneyReward * _maxRewardMultiply);
                    break;
                case (int)Ids.Five:
                    _bank.MoneyMultiplyAd();
                    break;
                case (int)Ids.Six:
                    _resurrect.ResurrectWatch();
                    break;
                case (int)Ids.Seven:
                    _energyBoost.RewardBoost();
                    break;
                case (int)Ids.Eight:
                    _moneyBoost.RewardBoost();
                    break;
                case (int)Ids.Nine:
                    _speedBoost.RewardBoost();
                    break;
                case (int)Ids.Ten:
                    _energyBoost.RewardUpgradeBoost();
                    break;
                case (int)Ids.Eleven:
                    _moneyBoost.RewardUpgradeBoost();
                    break;
                case (int)Ids.Twelve:
                    _speedBoost.RewardUpgradeBoost();
                    break;
            }
        }

        private void RefreshAdButtons()
        {
            _energyBoost.SelectAdButtons();
            _moneyBoost.SelectAdButtons();
            _speedBoost.SelectAdButtons();
            _moneyReward.RefreshAmountButton();
        }
    }
}
