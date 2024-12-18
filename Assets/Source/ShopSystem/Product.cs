using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace ShopSystem
{
    [RequireComponent(typeof(Button))]
    public abstract class Product : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private Image _buyFlag;
        [SerializeField] private Image _selectFlag;
        [SerializeField] private int _id;
        [SerializeField] private string _descriptionTranslation;
        [SerializeField] private bool _isSelected;
        [SerializeField] private TMP_Text _priceText;

        private Button _showButton;

        public event Action<Product> Selected;

        public string Description => _descriptionTranslation;
        public bool IsSelected => _isSelected;
        public int Price => _price;
        public int Id => _id;
        public bool IsBought { get; private set; } = false;

        private void Awake()
        {
            _showButton = GetComponent<Button>();
            _showButton.onClick.AddListener(ShowInfo);
            _buyFlag.gameObject.SetActive(IsBought);
            _priceText.text = $"{_price}";
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadText;
        }

        private void OnDisable()
        {
            _showButton.onClick?.RemoveListener(ShowInfo);
            YandexGame.GetDataEvent -= LoadText;
        }

        public void Unlock()
        {
            IsBought = true;
            _buyFlag.gameObject.SetActive(IsBought);
            _priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("Bought");
        }

        public void ShowInfo()
        {
            Selected?.Invoke(this);
        }

        public void ChangeStatus()
        {
            _isSelected = !_isSelected;
            _selectFlag.gameObject.SetActive(_isSelected);
        }

        private void LoadText()
        {
            _descriptionTranslation = Lean.Localization.LeanLocalization.GetTranslationText(_descriptionTranslation);
        }
    }
}