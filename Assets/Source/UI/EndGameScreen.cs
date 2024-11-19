using Audio;
using BankSystem;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class EndGameScreen : MonoBehaviour
    {
        private readonly int _id = 5;

        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Bank _bank;

        private int _chanceRefresh = 10;
        private float _timeTurnOn = 0.3f;
        private float _timeTurnOff = 1.5f;

        private void Start()
        {
            RefreshAdButton();
            _rewardButton.interactable = true;
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(CloseEndScreen);
            _rewardButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(CloseEndScreen);
            _rewardButton.onClick.RemoveListener(OnClick);
        }

        public void CloseEndScreen()
        {
            _bank.ResetValueForGame();
            RefreshAdButton();
        }

        private void RefreshAdButton()
        {
            int chance = Random.Range(0, 100);

            if (chance <= _chanceRefresh)
                _rewardButton.gameObject.SetActive(true);
            else
                _rewardButton.gameObject.SetActive(false);
        }

        private void OnClick()
        {
            YandexGame.RewVideoShow(_id);
            _rewardButton.interactable = false;
            Invoke(nameof(TurnOnConfetti), _timeTurnOn);
        }

        private void TurnOnConfetti()
        {
            _rewardButton.GetComponentInChildren<ParticleSystem>().Play();
            SoundSwitcher.Instance.Play("Confetti");
            Invoke(nameof(TurnOffObject), _timeTurnOff);
        }

        private void TurnOffObject()
        {
            _rewardButton?.gameObject.SetActive(false);
            _rewardButton.interactable = true;
        }
    }
}