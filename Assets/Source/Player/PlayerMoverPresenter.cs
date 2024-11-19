using UnityEngine;

namespace Player
{
    public class PlayerMoverPresenter : MonoBehaviour
    {
        private PlayerMoverModel _model;
        private PlayerMoverView _view;

        private void FixedUpdate()
        {
            _model?.Update();
        }

        public void Init(PlayerMoverModel model, PlayerMoverView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.Moving += SetDataMove;
            _view.SpeedCrashChanging += OnChangeSpeedCrash;
            _view.SpeedBoostChanging += OnSpeedChanging;
            _view.Somersaulting += OnSomerslaut;
            _view.InputChanging += _model.SetInput;
            _model.SpeedChanging += _view.ChangeCurrentSpeed;
            _model.BoostTimeChanging += _view.SetSpeedBoostTimer;
            _view.Jumping += OnJump;
            _model.Jumped += _view.OnJumped;
        }

        public void Disable()
        {
            _view.Moving -= SetDataMove;
            _view.SpeedCrashChanging -= OnChangeSpeedCrash;
            _view.SpeedBoostChanging -= OnSpeedChanging;
            _view.Somersaulting -= OnSomerslaut;
            _view.InputChanging -= _model.SetInput;
            _model.SpeedChanging -= _view.ChangeCurrentSpeed;
            _model.BoostTimeChanging -= _view.SetSpeedBoostTimer;
            _view.Jumping -= OnJump;
            _model.Jumped -= _view.OnJumped;
        }

        public void EndPlayerMove()
        {
            _model.EndMove();
            _view.EndMove();
        }

        public void ResetPlayerMove()
        {
            _model.ResetMove();
            _view.ResetMove();
        }

        public void StartPlayerMove()
        {
            _model.StartMove();
            _view.StartMove();
        }

        private void SetDataMove(float coefficient)
        {
            _model.SetDataMove(coefficient);
        }

        private void OnJump()
        {
            _model.Jump();
        }

        private void OnSomerslaut()
        {
            _model.Somersault();
        }

        private void OnChangeSpeedCrash(float moveSpeed)
        {
            _model.ChangeSpeedCrash(moveSpeed);
        }

        private void OnSpeedChanging(float bonus, float time)
        {
            _model.TurnOnSpeedBoost(bonus, time);
        }
    }
}