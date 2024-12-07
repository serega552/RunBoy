using System;
using Audio;
using BoostSystem;
using Chunks;
using InputSystem;
using Tasks;
using Tasks.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMoverView : MonoBehaviour
    {
        private readonly float _yRaySum = 0.2f;
        private readonly float _distanceRay = 0.3f;

        [SerializeField] private Joystick _joystick;
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private Boost _speedBoost;
        [SerializeField] private GameObject _prefabForDanceShop;
        [SerializeField] private Button _jumpButton;
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private SoundSwitcher _soundSwitcher;

        private Vector3 _startPlayerPosition;
        private string _nameDanceAnim;
        private Button _speedBoostButton;
        private float _speed;
        private bool _isProtected;
        private bool _canMove = false;
        private bool _canJump = true;
        private PlayerView _playerView;

        public event Action<PlayerInputHandler> InputChanging;

        public event Action<float> Moving;

        public event Action<float> SpeedChanging;

        public event Action<float> SpeedCrashChanging;

        public event Action<float, float> SpeedBoostChanging;

        public event Action<bool> Protected;

        public event Action Started;

        public event Action Stoped;

        public event Action Kicked;

        public event Action Jumped;

        public event Action Jumping;

        public event Action Somersaulting;

        public event Action Crashed;

        public event Action Restarting;

        public event Action Dancing;

        public string NameDanceAnim => _nameDanceAnim;
        public float CurrentSpeed => _speed;

        private void Start()
        {
            InputChanging?.Invoke(_inputHandler);
        }

        private void Awake()
        {
            _startPlayerPosition = transform.position;
            _speedBoostButton = _speedBoost.GetComponent<Button>();
            _playerView = GetComponent<PlayerView>();
        }

        private void Update()
        {
            if (_canMove)
            {
                PlayerInputContorol();
            }

            CheckGrounded();
        }

        private void OnEnable()
        {
            _cameraMover.AddPlayerTransform(transform);
            _speedBoostButton.onClick.AddListener(UseSpeedBoost);
            _inputHandler.JumpButtonClicking += Jump;
            _jumpButton.onClick.AddListener(Jump);
        }

        private void OnDisable()
        {
            _speedBoostButton.onClick.RemoveListener(UseSpeedBoost);
            _inputHandler.JumpButtonClicking -= Jump;
            _jumpButton.onClick.RemoveListener(Jump);
        }

        public GameObject GetPrefab()
        {
            return _prefabForDanceShop;
        }

        public void SetNameDance(string nameDance)
        {
            _nameDanceAnim = nameDance;
        }

        public void ChangeSpeed(float count, float time)
        {
            _soundSwitcher.Play("UseBoost");
            SpeedBoostChanging?.Invoke(count, time);
        }

        public void ChangeCurrentSpeed(float speed)
        {
            _speed = speed;
            SpeedChanging?.Invoke(speed);
        }

        public void Protect(bool protect)
        {
            _isProtected = protect;
            Protected?.Invoke(protect);
        }

        public void Crash()
        {
            if (_isProtected == false)
            {
                float moveSpeed = 3;
                _soundSwitcher.Play("Crash");
                SpeedCrashChanging?.Invoke(moveSpeed);

                TaskCounter.IncereaseProgress(1, TaskType.CrashWall.ToString());
            }
        }

        public void CrashOnCar()
        {
            if (_isProtected == false)
            {
                Crashed?.Invoke();
                _playerView.GameOver();
            }
        }

        public void StartMove()
        {
            _cameraMover?.StartMove();
            _canMove = true;
            Started?.Invoke();
        }

        public void ResetMove()
        {
            _cameraMover?.ResetCameraPosition();
            transform.position = _startPlayerPosition;
            Restarting?.Invoke();
        }

        public void EndMove()
        {
            _cameraMover?.EndMove();
            _canMove = false;
            Stoped?.Invoke();
            _isProtected = false;
        }

        public void OnKick()
        {
            Kicked?.Invoke();
        }

        public void OnJumped()
        {
            Jumped?.Invoke();
        }

        public void OnSomersault()
        {
            Somersaulting?.Invoke();
        }

        public void OnDance()
        {
            Dancing?.Invoke();
        }

        public void SetSpeedBoostTimer(float time)
        {
            _speedBoost.SetTimeText(time);
        }

        private void CheckGrounded()
        {
            RaycastHit hit;
            Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + _yRaySum, transform.position.z), transform.up * -1);

            if (Physics.Raycast(ray, out hit, _distanceRay))
            {
                if (hit.collider.TryGetComponent(out Chunk chunk))
                    _canJump = true;
            }
            else
            {
                _canJump = false;
            }
        }

        private void PlayerInputContorol()
        {
            if (_joystick.Horizontal < 0f && _joystick.Horizontal > -1)
                Moving?.Invoke(_joystick.Horizontal);
            else if (_joystick.Horizontal > 0f && _joystick.Horizontal < 1)
                Moving?.Invoke(_joystick.Horizontal);
            else
                Moving?.Invoke(0);
        }

        private void Jump()
        {
            if (_canJump && _canMove)
            {
                Jumping?.Invoke();
            }
        }

        private void UseSpeedBoost()
        {
            if (_speedBoost.TryUse())
                SpeedBoostChanging?.Invoke(_speedBoost.Bonus, _speedBoost.Time);
        }
    }
}