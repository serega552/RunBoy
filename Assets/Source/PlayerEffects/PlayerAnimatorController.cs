using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private readonly int RunState = Animator.StringToHash("RunState");
    private readonly int CrashState = Animator.StringToHash("CrashState");
    private readonly int CrashOnCarState = Animator.StringToHash("CrashOnCarState");
    private readonly int JumpState = Animator.StringToHash("JumpState");
    private readonly int LoseState = Animator.StringToHash("LoseState");
    private readonly int KickState = Animator.StringToHash("Kick");
    private readonly int FlipState = Animator.StringToHash("FlipState");
    private readonly int IdleState = Animator.StringToHash("IdleState");

    private Animator _animator;
    private PlayerMoverView _playerMoverView;
    private int _danceState;

    private void OnDisable()
    {
        _playerMoverView.OnDance -= Dance;
        _playerMoverView.OnRestart -= ResetPlayer;
        _playerMoverView.OnStarted -= Run;
        _playerMoverView.OnJumped -= Jump;
        _playerMoverView.OnKicked -= Kick;
        _playerMoverView.OnChangingSpeedCrash -= Crash;
        _playerMoverView.OnStoped -= Lose;
        _playerMoverView.OnSomersault -= Somersault;
        _playerMoverView.OnCrashed -= CrashOnCar;
    }

    public void Init(PlayerMoverView playerMoverView, Animator animator)
    {
        _playerMoverView = playerMoverView;
        _animator = animator;

        _playerMoverView.OnDance += Dance;
        _playerMoverView.OnRestart += ResetPlayer;
        _playerMoverView.OnStarted += Run;
        _playerMoverView.OnJumped += Jump;
        _playerMoverView.OnKicked += Kick;
        _playerMoverView.OnChangingSpeedCrash += Crash;
        _playerMoverView.OnStoped += Lose;
        _playerMoverView.OnSomersault += Somersault;
        _playerMoverView.OnCrashed += CrashOnCar;
    }

    private void Run()
    {
        _animator.Play(RunState);
    }

    private void Jump()
    {
        _animator.Play(JumpState);
    }

    private void Kick()
    {
        _animator.Play(KickState);
    }

    private void Crash(float speed)
    {
        _animator.Play(CrashState);
    }

    private void CrashOnCar()
    {
        _animator.Play(CrashOnCarState);
    }

    private void Somersault()
    {
        _animator.Play(FlipState);
    }

    private void Lose()
    {
        _animator.Play(LoseState);
    }

    private void Dance()
    {
        _danceState = Animator.StringToHash(_playerMoverView.NameDanceAnim);
        _animator.Play(_danceState);
    }

    private void ResetPlayer()
    {
        _animator.Play(IdleState);
    }
}
