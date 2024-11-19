using BankSystem;
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
                case 1:
                    _bank.GiveMoney(_countMoneyReward);
                    break;
                case 2:
                    _bank.GiveMoney(_countMoneyReward * _minRewardMultiply);
                    break;
                case 3:
                    _bank.GiveMoney(_countMoneyReward * _midleRewardMultiply);
                    break;
                case 4:
                    _bank.GiveMoney(_countMoneyReward * _maxRewardMultiply);
                    break;
                case 5:
                    _bank.MoneyMultiplyAd();
                    break;
                case 6:
                    _resurrect.ResurrectWatch();
                    break;
                case 7:
                    _energyBoost.RewardBoost();
                    break;
                case 8:
                    _moneyBoost.RewardBoost();
                    break;
                case 9:
                    _speedBoost.RewardBoost();
                    break;
                case 10:
                    _energyBoost.RewardUpgradeBoost();
                    break;
                case 11:
                    _moneyBoost.RewardUpgradeBoost();
                    break;
                case 12:
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
