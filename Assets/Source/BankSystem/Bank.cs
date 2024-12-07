using System;
using Audio;
using System.Collections.Generic;
using Tasks;
using Tasks.SO;
using TMPro;
using UnityEngine;
using YG;

namespace BankSystem
{
    public class Bank : MonoBehaviour
    {
        private readonly int _moneyForGameMultiply = 2;

        [SerializeField] private List<TMP_Text> _moneyText;
        [SerializeField] private List<TMP_Text> _diamondText;
        [SerializeField] private List<TMP_Text> _moneyForGameText;
        [SerializeField] private SoundSwitcher _soundSwitcher;

        private int _moneyForGame;
        private string _moneyName = "Money";
        private string _diamondName = "Diamond";

        public event Action Bought;

        public int Diamond { get; private set; } = 0;
        public int Money { get; private set; } = 0;

        private void Awake()
        {
            if (YandexGame.SDKEnabled == true)
            {
                Load();
            }
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += Load;
            AwardGiver.Rewarding += GiveReward;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Load;
            AwardGiver.Rewarding -= GiveReward;
        }

        public void UpdateText()
        {
            foreach (TMP_Text money in _moneyText)
            {
                money.text = Money.ToString();
            }

            foreach (TMP_Text diamond in _diamondText)
            {
                diamond.text = Diamond.ToString();
            }

            foreach (TMP_Text money in _moneyForGameText)
            {
                money.text = _moneyForGame.ToString();
            }

            Save();
        }

        public void AddCurrency(int currency, string name)
        {
            if(name == _moneyName)
                Money += currency;

            if (name == _diamondName)
                Diamond += currency;

            UpdateText();
        }

        public void AddMoneyForGame(int money)
        {
            _moneyForGame += money;
            Money += money;
            UpdateText();

            TaskCounter.IncereaseProgress(Convert.ToInt32(money), TaskType.CollectMoney.ToString());
        }

        public void MoneyMultiplyAd()
        {
            AddCurrency(_moneyForGame, Convert.ToString(ResourceType.Money));
            _moneyForGame *= _moneyForGameMultiply;
        }

        public void ResetValueForGame()
        {
            _moneyForGame = 0;
            UpdateText();
        }

        public bool CanTakeCurrency(int currency, int price)
        {
            return currency >= price;
        }

        public void TryTakeDiamond(int diamond)
        {
            if (CanTakeCurrency(Diamond, diamond))
            {
                Diamond -= diamond;
                UpdateText();
            }
        }

        public void TryTakeMoney(int money)
        {
            if (CanTakeCurrency(Money, money))
            {
                Money -= money;
                TaskCounter.IncereaseProgress(money, Convert.ToString(TaskType.SpendMoney));
                _soundSwitcher.Play("Buy");
                Bought?.Invoke();
                UpdateText();
            }
        }

        private void GiveReward(string name, int amount)
        {
            if (name == Convert.ToString(ResourceType.Money))
            {
                AddCurrency(amount, Convert.ToString(ResourceType.Money));
            }
            else if (name == Convert.ToString(ResourceType.Diamond))
            {
                AddCurrency(amount, Convert.ToString(ResourceType.Money));
            }
        }

        private void Save()
        {
            YandexGame.savesData.Money = Money;
            YandexGame.savesData.Diamond = Diamond;
            YandexGame.SaveProgress();
        }

        private void Load()
        {
            Money = YandexGame.savesData.Money;
            Diamond = YandexGame.savesData.Diamond;

            UpdateText();
        }
    }
}