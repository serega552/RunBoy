using UnityEngine;
using UnityEngine.UI;

public class EndScreenButton : MonoBehaviour
{
    [SerializeField] private Window _windowForOpen;

    private EndScreenWindow _endScreenWindow;
    private Button _openButton;

    private void Awake()
    {
        _openButton = GetComponent<Button>();
        _endScreenWindow = GetComponentInParent<EndScreenWindow>(); 
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(PressOnButton);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(PressOnButton);
    }

    private void PressOnButton()
    {
        _endScreenWindow.Close();
        _windowForOpen.Open();
    }
}
