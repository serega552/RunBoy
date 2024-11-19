using BoostSystem;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerPresenter : MonoBehaviour
    {
        private PlayerModel _model;
        private PlayerView _view;

        public event Action GameEnded;

        private void Update()
        {
            _model?.Update(_view.transform);
        }

        public void Init(PlayerModel model, PlayerView view)
        {
            _model = model;
            _view = view;
        }

        public void StartGame()
        {
            _model.Init();
            _model.StartGame();
        }

        public void ResurrectPlayer(float energy)
        {
            _model.Resurrect(energy);
        }

        public float TakeTotalDistance()
        {
            return _model.TotalDistanceTraveled;
        }

        public void ResetPlayer()
        {
            _model.ResetGame(_view.transform);
        }

        public void Enable()
        {
            _model?.Init();

            _view.MaxEnergyChanging += OnMaxEnergyChanging;
            _view.DistanceBoostChanging += UseDistanceDistanceBoost;
            _model.EnergyChanged += OnEnergyChanging;
            _model.EnergyGoned += EndGame;
            _view.GameOvered += EndGame;
            _view.EnergyChanging += OnViewEnergyChanged;
            _model.DistanceChanging += OnDistanceChanging;
            _model.TimeChanging += _view.SetEnergyTime;
        }

        public void Disable()
        {
            _view.MaxEnergyChanging -= OnMaxEnergyChanging;
            _view.DistanceBoostChanging -= UseDistanceDistanceBoost;
            _model.EnergyChanged -= OnEnergyChanging;
            _model.EnergyGoned -= EndGame;
            _view.GameOvered -= EndGame;
            _model.DistanceChanging -= OnDistanceChanging;
            _view.EnergyChanging -= OnViewEnergyChanged;
            _model.TimeChanging -= _view.SetEnergyTime;
        }

        private void UseDistanceDistanceBoost(EnergyBoost energyBoost)
        {
            _model.TurnOnEnergyBoost(energyBoost.Bonus, energyBoost.Time);
        }

        private void OnEnergyChanging()
        {
            _view.SetEnergy(_model.CurrentEnergy);
        }

        private void OnDistanceChanging()
        {
            _view.SetDistance(_model.TotalDistanceTraveled);
        }

        private void OnViewEnergyChanged(float energyAmount)
        {
            _model.TakeEnergy(energyAmount);
        }

        private void OnMaxEnergyChanging(float maxEnergyAmount)
        {
            _model.ChangeMaxEnergy(maxEnergyAmount);
            UpdateUI();
        }

        private void UpdateUI()
        {
            _view.UpdateUI(_model.MaxEnergy);
        }

        private void EndGame()
        {
            GameEnded?.Invoke();
        }
    }
}