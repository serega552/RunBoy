using UnityEngine;
using Agava.WebUtility;

public class TestFocus : MonoBehaviour
{
    [SerializeField] private PauseWindow _pause;

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }
    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        if (_pause.IsPause == false)
        {
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }
    }

    private void OnInBackgroundChangeWeb(bool isBackgrond)
    {
        if (_pause.IsPause == false)
        {
            MuteAudio(isBackgrond);
            PauseGame(isBackgrond);
        }
    }

    private void MuteAudio(bool value)
    {
        AudioListener.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
