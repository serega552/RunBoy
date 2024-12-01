using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private readonly string _actionMapName = "Player";
        private readonly string _turn = "Turn";
        private readonly string _jump = "Jump";
        private readonly string _pause = "Pause";

        [SerializeField] private InputActionAsset _playerControls;

        private InputAction _turnAction;
        private InputAction _jumpAction;
        private InputAction _pauseAction;

        public event Action PauseButtonClicking;

        public event Action JumpButtonClicking;

        public Vector2 TurnInput { get; private set; }

        private void OnEnable()
        {
            _turnAction.Enable();
            _jumpAction.Enable();
            _pauseAction.Enable();
        }

        private void Awake()
        {
            _turnAction = _playerControls.FindActionMap(_actionMapName).FindAction(_turn);
            _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
            _pauseAction = _playerControls.FindActionMap(_actionMapName).FindAction(_pause);

            RegisterInputActions();
        }

        private void OnDisable()
        {
            _turnAction.Disable();
            _jumpAction.Disable();
            _pauseAction.Disable();
        }

        private void RegisterInputActions()
        {
            _turnAction.performed += context => TurnInput = context.ReadValue<Vector2>();
            _turnAction.canceled += context => TurnInput = Vector2.zero;

            _jumpAction.performed += context => JumpButtonClicking?.Invoke();

            _pauseAction.performed += context => PauseButtonClicking?.Invoke();
        }
    }
}