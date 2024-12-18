using ShopSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class ShopDancingWindow : Window
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private ShopBoostWindow _shopBoosts;
        [SerializeField] private ShopSkinsWindow _shopSkins;

        private Shop _shop;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(Open);
        }

        private void Awake()
        {
            CloseWithoutSound();
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
            _shopSkins.Close();
        }

        public override void Close()
        {
            CloseWithoutSound();
            _shop.TurnOffModel();
        }
    }
}