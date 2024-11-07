using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResetGameWindow : Window
{
    [SerializeField] private Button _resetGameButton;
    [SerializeField] private Button _openWindow;
    [SerializeField] private Button _closeWindow;
    [SerializeField] private GameObject _restartGameWindow;

    private void Start()
    {
        CloseWithoutSound();
        _restartGameWindow.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _resetGameButton.onClick.AddListener(ResetSaves);
        _openWindow.onClick.AddListener(Open);
        _closeWindow.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _resetGameButton.onClick.RemoveListener(ResetSaves);
        _openWindow.onClick.RemoveListener(Open);
        _closeWindow.onClick.RemoveListener(Close);
    }

    private void ResetSaves()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
        _restartGameWindow.gameObject.SetActive(true);
    }
}
