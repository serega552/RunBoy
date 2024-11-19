using BankSystem;
using Tasks;
using Tasks.SO;
using TMPro;
using UnityEngine;
using YG;

namespace UpgradeEnergy
{
    public class EnergyUpgrade : MonoBehaviour
    {
        private readonly int _maxCountEnergy = 100;
        private readonly int _maxPrice = 300;
        private readonly int _encreaceEnergy = 10;
        private readonly int _encreaceMoney = 5;

        [SerializeField] private TMP_Text _currentPrice;
        [SerializeField] private Bank _bank;

        private int _count;

        public int CurrentPrice { get; private set; } = 10;
        public float CurrentEnergy { get; private set; } = 0;

        private void OnEnable() => YandexGame.GetDataEvent += Load;

        private void OnDisable() => YandexGame.GetDataEvent -= Load;

        private void Awake()
        {
            if (YandexGame.SDKEnabled == true)
                Load();
        }

        public float Upgrade()
        {
            if (_bank.TryTakeMoney(CurrentPrice))
            {
                _bank.TakeMoney(CurrentPrice);

                if (CurrentPrice < _maxPrice)
                    CurrentPrice += _encreaceMoney;
                else
                    CurrentPrice = _maxPrice;

                UpdateUI();

                TaskCounter.IncereaseProgress(1, TaskType.UpgradeEnergy.ToString());

                if (CurrentEnergy < _maxCountEnergy)
                {
                    CurrentEnergy += _encreaceEnergy;
                    _count++;
                    Save();
                    return CurrentEnergy;
                }
                else
                {
                    CurrentEnergy = _maxCountEnergy;
                    _count++;
                    Save();
                    return CurrentEnergy;
                }
            }
            else
            {
                return 0;
            }
        }

        private void UpdateUI()
        {
            _currentPrice.text = CurrentPrice.ToString();
        }

        private void Save()
        {
            YandexGame.savesData.CountUpgradeEnergy = _count;
            YandexGame.SaveProgress();
        }

        private void Load()
        {
            _count = YandexGame.savesData.CountUpgradeEnergy;

            CurrentPrice += _encreaceMoney * _count;
            CurrentEnergy += _encreaceEnergy * _count;

            if (CurrentPrice >= _maxPrice)
                CurrentPrice = _maxPrice;

            if (CurrentEnergy >= _maxCountEnergy)
                CurrentEnergy = _maxCountEnergy;

            UpdateUI();
        }
    }
}