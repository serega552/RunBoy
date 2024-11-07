using UnityEngine;
using UnityEngine.UI;
using YG;

public class AuthWindow : Window
{
    [SerializeField] private Button _openAuthDialog;
    [SerializeField] private Button _closeWindow;

    private void OnEnable()
    {
        _openAuthDialog.onClick.AddListener(OpenAuthDialog);
        _closeWindow.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openAuthDialog.onClick.RemoveListener(OpenAuthDialog);
        _closeWindow.onClick.RemoveListener(Close);
    }

    private void OpenAuthDialog()
    {
            YandexGame.AuthDialog();
            Close();
    }
}
