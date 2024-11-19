using Audio;
using System;
using Tasks;
using Tasks.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Windows;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _recordDistanceText;

        private float _recordDistance;
        private MenuWindow _menuWindow;

        public event Action StartClicking;

        private void Start()
        {
            SoundSwitcher.Instance.Play("Music2");
        }

        private void Awake()
        {
            _menuWindow = GetComponent<MenuWindow>();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnClick);
        }

        public void SetDistance(float distance)
        {
            if (distance > _recordDistance)
            {
                _recordDistance = distance;
                _recordDistanceText.text = $"{Convert.ToInt32(_recordDistance)}";
                TaskCounter.IncereaseProgress(Convert.ToInt32(_recordDistance), TaskType.RecordDistance.ToString());
            }
        }

        private void OnClick()
        {
            StartClicking?.Invoke();
            _menuWindow.Close();
        }
    }
}