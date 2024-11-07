using UnityEngine;

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
        _view.OnMoving += SetDataMove;
        _view.OnChangingSpeedCrash += ChangeSpeedCrash;
        _view.OnSpeedBoostChanging += OnSpeedChanging;
        _view.OnSomersault += Somerslaut;
        _model.OnChangeSpeed += _view.ChangeCurrentSpeed;
        _model.OnChangingBoostTime += _view.SetSpeedBoostTimer;
        _view.OnJumping += Jump;
        _model.OnJumped += _view.Jumped;
    }

    public void Disable()
    {
        _view.OnMoving -= SetDataMove;
        _view.OnChangingSpeedCrash -= ChangeSpeedCrash;
        _view.OnSpeedBoostChanging -= OnSpeedChanging;
        _view.OnSomersault -= Somerslaut;
        _model.OnChangeSpeed -= _view.ChangeCurrentSpeed;
        _model.OnChangingBoostTime -= _view.SetSpeedBoostTimer;
        _view.OnJumping -= Jump;
        _model.OnJumped -= _view.Jumped;
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

    private void Jump()
    {
        _model.Jump();
    }

    private void Somerslaut()
    {
        _model.Somersault();
    }

    private void ChangeSpeedCrash(float moveSpeed)
    {
        _model.ChangeSpeedCrash(moveSpeed);
    }

    private void OnSpeedChanging(float bonus, float time)
    {
        _model.TurnOnSpeedBoost(bonus, time);
    }
}
