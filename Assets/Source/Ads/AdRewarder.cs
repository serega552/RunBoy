using BankSystem;
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
        private int _minChance = 20;
        private int _maxChance = 30;
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

            if (_bank.Money <= _moneyTight && chance <= _maxChance)
            {
                RefreshAmountPrice(1);
                _id = 1;
            }
            else if (_bank.Money <= _moneyNormal && chance <= _minChance)
            {
                RefreshAmountPrice(_minRewardMultiply);
                _id = 2;
            }
            else if (_bank.Money <= _muchMoney && chance <= _minChance)
            {
                RefreshAmountPrice(_midleRewardMultiply);
                _id = 3;
            }
            else if (_bank.Money >= _muchMoney && chance <= _minChance)
            {
                RefreshAmountPrice(_maxRewardMultiply);
                _id = 4;
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