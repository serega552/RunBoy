using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private MenuWindow _mainMenu;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private ShopBoostWindow _boostWindow;
    private ShopDancingWindow _dancingWindow;
    private ShopSkinsWindow _skinsWindow;

    private void Awake()
    {
        CloseWithoutSound();
        _boostWindow = GetComponentInChildren<ShopBoostWindow>();
        _dancingWindow = GetComponentInChildren<ShopDancingWindow>();
        _skinsWindow = GetComponentInChildren<ShopSkinsWindow>();
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
        _mainMenu.CloseWithoutSound();
        _skinsWindow.Open();
    }

    public override void Close()
    {
        base.Close();
        _mainMenu.OpenWithoutSound();

        _boostWindow.CloseWithoutSound();
        _dancingWindow.Close();
        _skinsWindow.Close();
    }
}
