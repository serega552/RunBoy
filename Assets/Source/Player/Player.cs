using BankSystem;
using BoostSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private bool _isMoneyBoost = false;
        private float _moneyBoostTime;
        private MoneyBoost _moneyBoost;
        private Bank _bank;
        private EnergyBoost _energyBoost;
        private PlayerEnergy _playerEnergy;

        public event Action<float, bool> MoneyChanging;

        public void Init(MoneyBoost moneyBoost, Bank bank, EnergyBoost energyBoost, PlayerEnergy playerEnergy )
        {
            _moneyBoost = moneyBoost;
            _bank = bank;
            _energyBoost = energyBoost;
            _playerEnergy = playerEnergy;
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

        public void UseMoneyBoost()
        {
            if (_moneyBoost.TryUse())
            {
                _isMoneyBoost = true;
                _moneyBoostTime = _moneyBoost.Time;
                StartCoroutine(TimeChanging());
            }
        }

        public void UseEnergyBoost()
        {
            if (_energyBoost.TryUse())
                _playerEnergy.TurnOnEnergyBoost(_energyBoost.Bonus, _energyBoost.Time);
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

