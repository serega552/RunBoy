using UnityEngine;

public class FunDance : MonoBehaviour
{
    private readonly int DanceCamera = Animator.StringToHash("DanceCamera");
    private readonly int IdleState = Animator.StringToHash("Idle");

    [SerializeField] private GameObject _enemyPolice;
    [SerializeField] private Camera _danceCamera;
    [SerializeField] private CanvasGroup _info;

    private Animator _danceCameraAnimator;
    private Animator _enemyAnimator;
    private PlayerMoverView _playerMoverView;

    private void Awake()
    {
        _danceCameraAnimator = _danceCamera.GetComponent<Animator>();
        _enemyAnimator = _enemyPolice.GetComponent<Animator>();
        _danceCamera.gameObject.SetActive(false);
    }

    public void Init(PlayerMoverView player)
    {
        _playerMoverView = player;
    }

    public void TurnOnDance()
    {
        AudioManager.Instance.Play("FunDance");
        AudioManager.Instance.Pause("Music2");

        _info.alpha = 0f;
        _playerMoverView.Dance();
        _danceCamera.gameObject.SetActive(true);

        _enemyAnimator.Play(_playerMoverView.NameDanceAnim);
        _danceCameraAnimator.Play(DanceCamera);
    }

    public void TurnOffDance()
    {
        AudioManager.Instance.Stop("FunDance");
        AudioManager.Instance.UnPause("Music2");

        _info.alpha = 1f;
        _playerMoverView.ResetMove();
        _danceCamera.gameObject.SetActive(false);

        _enemyAnimator.Play(IdleState);
        _danceCameraAnimator.Play(IdleState);
    }
}
