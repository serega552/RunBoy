using Audio;
using Player;
using UnityEngine;

namespace FunDanceSystem
{
    public class FunDance : MonoBehaviour
    {
        private readonly int _danceCameraAnim = Animator.StringToHash("DanceCamera");
        private readonly int _idleState = Animator.StringToHash("Idle");

        [SerializeField] private GameObject _enemyPolice;
        [SerializeField] private Camera _danceCamera;
        [SerializeField] private CanvasGroup _info;
        [SerializeField] private SoundSwitcher _soundSwitcher;

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
            _soundSwitcher.Play("FunDance");
            _soundSwitcher.Pause("Music2");

            _info.alpha = 0f;
            _playerMoverView.OnDance();
            _danceCamera.gameObject.SetActive(true);

            _enemyAnimator.Play(_playerMoverView.NameDanceAnim);
            _danceCameraAnimator.Play(_danceCameraAnim);
        }

        public void TurnOffDance()
        {
            _soundSwitcher.Stop("FunDance");
            _soundSwitcher.UnPause("Music2");

            _info.alpha = 1f;
            _playerMoverView.ResetMove();
            _danceCamera.gameObject.SetActive(false);

            _enemyAnimator.Play(_idleState);
            _danceCameraAnimator.Play(_idleState);
        }
    }
}