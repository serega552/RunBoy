using Audio;
using BankSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Windows;
using YG;

namespace UI
{
    public class PlayerResurrect : MonoBehaviour
    {
        private readonly float _energyGiftForWatch = 200;
        private readonly float _energyGiftForDiamond = 100;
        private readonly int _id = 6;
        private readonly int _priceMultiply = 2;

        [SerializeField] private TMP_Text _priceDiamondText;
        [SerializeField] private Button _diamondContinue;
        [SerializeField] private Button _watchContinue;
        [SerializeField] private Bank _bank;
        [SerializeField] private Button _exitButton;

        private int _price = 1;
        private PlayerResurrectWindow _playerResurrectWindow;

        public event Action Restarting;
        public event Action<float> Resurrected;

        private void Awake()
        {
            _playerResurrectWindow = GetComponent<PlayerResurrectWindow>();
            _priceDiamondText.text = _price.ToString();
        }

        private void OnEnable()
        {
            _diamondContinue.onClick.AddListener(DiamondResurrect);
            _watchContinue.onClick.AddListener(OnResurrect);
            _exitButton.onClick.AddListener(ExitToMenu);
        }

        private void OnDisable()
        {
            _diamondContinue.onClick.RemoveListener(DiamondResurrect);
            _watchContinue.onClick.RemoveListener(OnResurrect);
            _exitButton.onClick.RemoveListener(ExitToMenu);
        }

        public void OpenWindow()
        {
            _playerResurrectWindow.OpenWithoutSound();
        }

        public void ResurrectWatch()
        {
            _price *= _priceMultiply;
            _priceDiamondText.text = _price.ToString();
            Resurrect(_energyGiftForWatch);
        }

        private void OnResurrect()
        {
            YandexGame.RewVideoShow(_id);
        }

        private void DiamondResurrect()
        {
            if (_bank.TryTakeDiamond(_price))
            {
                _bank.TakeDiamond(_price);
                _price *= _priceMultiply;
                _priceDiamondText.text = _price.ToString();
                Resurrect(_energyGiftForDiamond);
            }
        }

        private void Resurrect(float energy)
        {
            SoundSwitcher.Instance.Play("Ressurect");
            SoundSwitcher.Instance.UnPause("Music");

            _playerResurrectWindow.CloseWithoutSound();
            Resurrected?.Invoke(energy);
        }

        private void ExitToMenu()
        {
            _playerResurrectWindow.CloseWithoutSound();
            _price = 1;
            _priceDiamondText.text = _price.ToString();
            Restarting?.Invoke();
            YandexGame.FullscreenShow();
        }
    }
}