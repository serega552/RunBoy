using BankSystem;
using IdNumbers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ads
{
    public class AdRewarder : MonoBehaviour
    {
        private readonly int _moneyTight = 150;
        private readonly int _moneyNormal = 1500;
        private readonly int _muchMoney = 3000;
        private readonly int _rewardMoney = 200;

        [SerializeField] private Bank _bank;
        [SerializeField] private TMP_Text _amountRewardText;
        [SerializeField] private Button _rewardButton;

        private int _amountReward;
        private int _id;
        private int _chance = 30;
        private int _minRewardMultiply = 3;
        private int _midleRewardMultiply = 5;
        private int _maxRewardMultiply = 8;

        private void Awake()
        {
            _rewardButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _rewardButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _rewardButton.onClick.RemoveListener(OnButtonClick);
        }

        public void RefreshAmountButton()
        {
            int chance = Random.Range(0, 100);

            if (chance <= _chance)
            {
                if (_bank.Money <= _moneyTight)
                {
                    RefreshAmountPrice(1);
                    _id = (int)Ids.One;
                }
                else if (_bank.Money <= _moneyNormal)
                {
                    RefreshAmountPrice(_minRewardMultiply);
                    _id = (int)Ids.Two;
                }
                else if (_bank.Money <= _muchMoney)
                {
                    RefreshAmountPrice(_midleRewardMultiply);
                    _id = (int)Ids.Three;
                }
                else if (_bank.Money >= _muchMoney)
                {
                    RefreshAmountPrice(_maxRewardMultiply);
                    _id = (int)Ids.Four;
                }
            }
            else
            {
                _rewardButton.gameObject.SetActive(false);
            }
        }

        private void RefreshAmountPrice(int multiPly)
        {
            _amountReward = _rewardMoney * multiPly;
            _amountRewardText.text = $"+{_amountReward}$";
            _rewardButton.gameObject.SetActive(true);
        }

        private void OnButtonClick()
        {
            YandexGame.RewVideoShow(_id);
            _rewardButton.gameObject.SetActive(false);
        }
    }
}