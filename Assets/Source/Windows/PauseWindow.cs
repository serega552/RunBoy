using Audio;
using InputSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class PauseWindow : Window
    {
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        private bool _isPause = false;

        public bool IsPause => _isPause;

        private void Start()
        {
            CloseWithoutSound();
            _inputHandler.PauseButtonClicking += TogglePause;
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(TogglePause);
            _closeButton.onClick.AddListener(TogglePause);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(TogglePause);
            _closeButton.onClick.RemoveListener(TogglePause);
            _inputHandler.PauseButtonClicking -= TogglePause;
        }

        public override void Open()
        {
            base.Open();
            Time.timeScale = 0f;
            SoundSwitcher.Pause("Music");
        }

        public override void Close()
        {
            base.Close();
            Time.timeScale = 1f;
            SoundSwitcher.UnPause("Music");
        }

        private void TogglePause()
        {
            _isPause = !_isPause;

            if (_isPause)
                Open();
            else
                Close();
        }
    }
}