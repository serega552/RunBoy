using UnityEngine;
using UnityEngine.UI;

public class ShopBoostWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private ShopDancingWindow _shopDancing;
    [SerializeField] private ShopSkinsWindow _shopSkins;

    private void Awake()
    {
        CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
    }

    public override void Open()
    {
        base.Open();

        _shopDancing.Close();
        _shopSkins.Close();
    }
}
