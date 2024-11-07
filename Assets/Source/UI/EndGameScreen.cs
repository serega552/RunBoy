using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Bank _bank;

    private int _id = 5;

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
        int chance = UnityEngine.Random.Range(0, 100);

        if (chance <= 10)
            _rewardButton.gameObject.SetActive(true);
        else
            _rewardButton.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        YandexGame.RewVideoShow(_id);
        _rewardButton.interactable = false;
        Invoke("TurnOnConfetti", 0.3f);
    }

    private void TurnOnConfetti()
    {
        _rewardButton.GetComponentInChildren<ParticleSystem>().Play();
        AudioManager.Instance.Play("Confetti");
        Invoke("TurnOffObject", 1.5f);
    }

    private void TurnOffObject()
    {
        _rewardButton?.gameObject.SetActive(false);
        _rewardButton.interactable=true;
    }
}
