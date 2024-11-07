using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerResurrect : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceDiamondText;
    [SerializeField] private Button _diamondContinue;
    [SerializeField] private Button _watchContinue;
    [SerializeField] private Bank _bank;
    [SerializeField] private Button _exitButton;

    private int _price = 1;
    private PlayerResurrectWindow _playerResurrectWindow;
    private float _energyGiftForWatch = 200;
    private float _energyGiftForDiamond = 100;
    private int _id = 6;

    public event Action OnRestarting;
    public event Action<float> OnResurrected;

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
        _price *= 2;
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
            _price *= 2;
            _priceDiamondText.text = _price.ToString();
            Resurrect(_energyGiftForDiamond);
        }       
    }

    private void Resurrect(float energy)
    {
        AudioManager.Instance.Play("Ressurect");
        AudioManager.Instance.UnPause("Music");

        _playerResurrectWindow.CloseWithoutSound();
        OnResurrected?.Invoke(energy);
    }

    private void ExitToMenu()
    {
        _playerResurrectWindow.CloseWithoutSound();
        _price = 1;
        _priceDiamondText.text = _price.ToString();
        OnRestarting?.Invoke();
        YandexGame.FullscreenShow();
    }
}
