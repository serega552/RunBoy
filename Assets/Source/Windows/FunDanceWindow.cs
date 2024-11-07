using UnityEngine;
using UnityEngine.UI;

public class FunDanceWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private FunDance _funDance;

    private void Awake()
    {
        CloseWithoutSound();
        _funDance = GetComponent<FunDance>();   
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }

    public override void Open()
    {
        base.Open();
        _funDance.TurnOnDance();
    } 
    
    public override void Close()
    {
        base.Close();
        _funDance.TurnOffDance();
    }
}
