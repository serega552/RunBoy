using System;
using Audio;
using BankSystem;
using BoostSystem;
using System.Collections;
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
        [SerializeField] private Player _player;

        private Button _energyBoostButton;
        private Button _moneyBoostButton;
        private Button _energyUpgradeButton;
        private bool _isMoneyBoost = false;
        private float _moneyBoostTime;

        public event Action GameEnded;
        public event Action<float, bool> MoneyChanging;

        private void Awake()
        {
            _moneyBoostButton = _moneyBoost.GetComponent<Button>();
            _energyBoostButton = _energyBoost.GetComponent<Button>();
            _energyUpgradeButton = _energyUpgrade.GetComponent<Button>();

            UpdateUI(0);
        }

        private void OnEnable()
        {
            _moneyBoostButton.onClick.AddListener(UseMoneyBoost);
            _energyBoostButton.onClick.AddListener(UseEnergyBoost);
            _energyUpgradeButton.onClick.AddListener(OnChangeMaxEnergy);
        }

        private void OnDisable()
        {
            _moneyBoostButton.onClick.RemoveListener(UseMoneyBoost);
            _energyBoostButton.onClick.RemoveListener(UseEnergyBoost);
            _energyUpgradeButton.onClick.RemoveListener(OnChangeMaxEnergy);
        }

        public void StartMove()
        {
            _player.StartGame();
        }

        public void ResurrectPlayer(float energy)
        {
            _player.Resurrect(energy);
        }

        public float TakeTotalDistance()
        {
            return _player.TotalDistanceTraveled;
        }

        public void ResetPlayer()
        {
            _player.ResetGame(transform);
        }

        public void EndMove()
        {
            GameEnded?.Invoke();
        }

        public void OnEnergyChanged(float energyAmount)
        {
            _soundSwitcher.Play("UseBoost");
            _player.ChangingEnergy(energyAmount);
        }

        public void OnChangeMaxEnergy()
        {
            _player.ChangeMaxEnergy(_energyUpgrade.Upgrade());
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

        public void AddMoney(int count, bool isBoost)
        {
            if (_isMoneyBoost || isBoost)
            {
                MoneyChanging?.Invoke(count * _moneyBoost.Bonus, isBoost);
                _bank.AddMoneyForGame(count * Convert.ToInt32(_moneyBoost.Bonus));
            }
            else
            {
                _bank.AddMoneyForGame(count);
            }
        }

        public void SetEnergyTime(float time)
        {
            _energyBoost.SetTimeText(time);
        }

        private void UseMoneyBoost()
        {
            if (_moneyBoost.TryUse())
            {
                _isMoneyBoost = true;
                _moneyBoostTime = _moneyBoost.Time;
                StartCoroutine(TimeChanging());
            }
        }

        private void UseEnergyBoost()
        {
            if (_energyBoost.TryUse())
                _player.TurnOnEnergyBoost(_energyBoost.Bonus, _energyBoost.Time);
        }

        private IEnumerator TimeChanging()
        {
            while (_isMoneyBoost)
            {
                _moneyBoostTime -= Time.deltaTime;
                _moneyBoost.SetTimeText(_moneyBoostTime);

                if (_moneyBoostTime <= 0)
                    _isMoneyBoost = false;

                yield return null;
            }
        }
    }
}