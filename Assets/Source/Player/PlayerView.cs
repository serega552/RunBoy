using System;
using Audio;
using BankSystem;
using BoostSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpgradeEnergy;
using Windows;
using YG;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _distance;
        [SerializeField] private TMP_Text _energy;
        [SerializeField] private TMP_Text _maxEnergy;
        [SerializeField] private HudWindow _headUpDisplay;
        [SerializeField] private EnergyBoost _energyBoost;
        [SerializeField] private MoneyBoost _moneyBoost;
        [SerializeField] private EnergyUpgrade _energyUpgrade;
        [SerializeField] private Bank _bank;
        [SerializeField] private SoundSwitcher _soundSwitcher;
        [SerializeField] private PlayerEnergy _playerEnergy;
        [SerializeField] private Player _player;

        private Button _energyBoostButton;
        private Button _moneyBoostButton;
        private Button _energyUpgradeButton;

        public event Action GameEnded;
        public event Action<float, bool> MoneyChanging;

        private void Awake()
        {
            _moneyBoostButton = _moneyBoost.GetComponent<Button>();
            _energyBoostButton = _energyBoost.GetComponent<Button>();
            _energyUpgradeButton = _energyUpgrade.GetComponent<Button>();

            _player.Init(_moneyBoost, _bank, _energyBoost, _playerEnergy);
            UpdateUI(0);
        }

        private void OnEnable()
        {
            _moneyBoostButton.onClick.AddListener(_player.UseMoneyBoost);
            _energyBoostButton.onClick.AddListener(_player.UseEnergyBoost);
            _energyUpgradeButton.onClick.AddListener(OnChangeMaxEnergy);
        }

        private void OnDisable()
        {
            _moneyBoostButton.onClick.RemoveListener(_player.UseMoneyBoost);
            _energyBoostButton.onClick.RemoveListener(_player.UseEnergyBoost);
            _energyUpgradeButton.onClick.RemoveListener(OnChangeMaxEnergy);
        }

        public void StartMove()
        {
            _playerEnergy.StartGame();
        }

        public void ResurrectPlayer(float energy)
        {
            _playerEnergy.Resurrect(energy);
        }

        public void TryToAddMoney(int count, bool isBoost)
        {
            MoneyChanging(count, isBoost);
            _player.AddMoney(count, isBoost);
        }

        public float TakeTotalDistance()
        {
            return _playerEnergy.TotalDistanceTraveled;
        }

        public void ResetPlayer()
        {
            _playerEnergy.ResetGame(transform);
        }

        public void EndMove()
        {
            GameEnded?.Invoke();
        }

        public void OnEnergyChanged(float energyAmount)
        {
            _soundSwitcher.Play("UseBoost");
            _playerEnergy.ChangingEnergy(energyAmount);
        }

        public void SetDistance(float distance)
        {
            _distance.text = $"{Convert.ToInt32(distance)}";
        }

        public void SetEnergy(float energyAmount)
        {
            _energy.text = $"{Convert.ToInt32(energyAmount)}";
        }

        public void UpdateUI(float maxEnergy)
        {
            _maxEnergy.text = maxEnergy.ToString();

            if (maxEnergy == 0)
                _maxEnergy.text = YandexGame.savesData.MaxEnergy.ToString();
        }

        private void OnChangeMaxEnergy()
        {
            _playerEnergy.ChangeMaxEnergy(_energyUpgrade.Upgrade());
        }
    }
}