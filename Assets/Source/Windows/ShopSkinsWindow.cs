using UnityEngine;
using UnityEngine.UI;

public class ShopSkinsWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private ShopDancingWindow _shopDancing;
    [SerializeField] private ShopBoostWindow _shopBoosts;

    private Shop _shop;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
    }

    private void Awake()
    {
        OpenWithoutSound();
        _shop = GetComponent<Shop>();
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
    }

    public override void Open()
    {
        base.Open();
        _shop.TurnOnModel();

        _shopBoosts.CloseWithoutSound();
        _shopDancing.Close();
    }

    public override void Close()
    {
        base.CloseWithoutSound();
        _shop.TurnOffModel();
    }
}
