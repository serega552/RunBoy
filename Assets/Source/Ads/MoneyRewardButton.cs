using BankSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ads
{
    public class MoneyRewardButton : MonoBehaviour
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

            if (_bank.Money <= _moneyTight && chance <= 30)
            {
                RefreshAmountPrice(_rewardMoney);
                _rewardButton.gameObject.SetActive(true);
                _id = 1;
            }
            else if (_bank.Money <= _moneyNormal && chance <= 20)
            {
                RefreshAmountPrice(_rewardMoney * 3);
                _rewardButton.gameObject.SetActive(true);
                _id = 2;
            }
            else if (_bank.Money <= _muchMoney && chance <= 20)
            {
                RefreshAmountPrice(_rewardMoney * 5);
                _rewardButton.gameObject.SetActive(true);
                _id = 3;
            }
            else if (_bank.Money >= _muchMoney && chance <= 20)
            {
                RefreshAmountPrice(_rewardMoney * 8);
                _rewardButton.gameObject.SetActive(true);
                _id = 4;
            }
            else
            {
                _rewardButton.gameObject.SetActive(false);
            }
        }

        private void RefreshAmountPrice(int amount)
        {
            _amountReward = amount;
            _amountRewardText.text = _amountReward.ToString();
        }

        private void OnButtonClick()
        {
            YandexGame.RewVideoShow(_id);
            _rewardButton.gameObject.SetActive(false);
        }
    }
}