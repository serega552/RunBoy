using System;
using UnityEngine;
using YG;

namespace Player
{
    public class PlayerModel
    {
        private Vector3 _lastPosition;
        private bool _isEnergyGone = false;
        private float _energyBonus;
        private float _energyTime;
        private bool _isEnergyBoost = false;

        public event Action DistanceChanging;
        public event Action EnergyGoned;
        public event Action EnergyChanged;
        public event Action<float> TimeChanging;

        public float TotalDistanceTraveled { get; private set; }
        public float MaxEnergy { get; private set; }
        public float CurrentEnergy { get; private set; }

        public void Init()
        {
            if (YandexGame.SDKEnabled)
                Load();
        }

        public void TakeEnergy(float count)
        {
            CurrentEnergy += count;
        }

        public void StartGame()
        {
            CurrentEnergy = MaxEnergy;
            _isEnergyGone = false;

            _isEnergyBoost = false;
            TimeChanging?.Invoke(0);
        }

        public void Resurrect(float energy)
        {
            CurrentEnergy = energy;
            _isEnergyGone = false;
        }

        public void ResetGame(Transform transformPosition)
        {
            _lastPosition = transformPosition.position;
            CurrentEnergy = MaxEnergy;
            TotalDistanceTraveled = 0;
        }

        public void Update(Transform transform)
        {
            AddEnergy(transform);
        }

        public void ChangingEnergy(float distanceMoved)
        {
            if (_isEnergyBoost == false)
            {
                CurrentEnergy -= distanceMoved;
            }
            else if (_isEnergyBoost)
            {
                _energyTime -= Time.deltaTime;
                TimeChanging?.Invoke(_energyTime);

                if (_energyTime > 0)
                {
                    distanceMoved /= _energyBonus;
                    CurrentEnergy -= distanceMoved;
                }
                else if (_energyTime <= 0)
                {
                    _isEnergyBoost = false;
                }
            }
        }

        public void TurnOnEnergyBoost(float bonus, float time)
        {
            _energyBonus = bonus;
            _energyTime = time;
            _isEnergyBoost = true;
        }

        public void ChangeMaxEnergy(float maxEnergyAmount)
        {
            MaxEnergy += maxEnergyAmount;
            Save();
        }

        private void AddEnergy(Transform transform)
        {
            if (CurrentEnergy > 0 && _isEnergyGone == false)
            {
                float distanceMoved = Vector3.Distance(transform.position, _lastPosition);
                TotalDistanceTraveled += distanceMoved;

                ChangingEnergy(distanceMoved);

                _lastPosition = transform.position;

                DistanceChanging?.Invoke();
                EnergyChanged?.Invoke();
            }

            if (CurrentEnergy <= 0 && _isEnergyGone == false)
            {
                _isEnergyGone = true;
                EnergyGoned?.Invoke();
            }
        }

        private void Save()
        {
            YandexGame.savesData.MaxEnergy = MaxEnergy;
            YandexGame.SaveProgress();
        }

        private void Load()
        {
            MaxEnergy = YandexGame.savesData.MaxEnergy;
            CurrentEnergy = MaxEnergy;
        }
    }
}