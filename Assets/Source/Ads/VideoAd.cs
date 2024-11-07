using UnityEngine;
using YG;

public class VideoAd : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    [SerializeField] private PlayerResurrect _resurrect;
    [SerializeField] private BoostBuyButton _energyBoost;
    [SerializeField] private BoostBuyButton _speedBoost;
    [SerializeField] private BoostBuyButton _moneyBoost;
    [SerializeField] private MoneyRewardButton _moneyReward;

    private void OnEnable()
    {
        _resurrect.OnRestarting += RefreshAdButtons;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        _resurrect.OnRestarting -= RefreshAdButtons;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void Rewarded(int id)
    {
        if (id == 1)
            _bank.GiveMoney(200);
        else if (id == 2)
            _bank.GiveMoney(200 * 3);
        else if (id == 3)
            _bank.GiveMoney(200 * 5);
        else if (id == 4)
            _bank.GiveMoney(200 * 8);
        else if (id == 5)
            _bank.MoneyMultiplyAd();
        else if (id == 6)
            _resurrect.ResurrectWatch();
        else if (id == 7)
            _energyBoost.RewardBoost();
        else if (id == 8)
            _moneyBoost.RewardBoost();
        else if (id == 9)
            _speedBoost.RewardBoost();
        else if (id == 10)
            _energyBoost.RewardUpgradeBoost();
        else if (id == 11)
            _moneyBoost.RewardUpgradeBoost();
        else if (id == 12)
            _speedBoost.RewardUpgradeBoost();
    }
    private void RefreshAdButtons()
    {
        _energyBoost.SelectAdButtons();
        _moneyBoost.SelectAdButtons();
        _speedBoost.SelectAdButtons();
        _moneyReward.RefreshAmountButton();
    }
}
