using BoostSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class BoostBuyButton : MonoBehaviour
    {
        private readonly int _maxUpgradeBoost = 5;
        private readonly int _minBoosts = 5;
        private readonly int _maxChance = 20;
        private readonly int _minChance = 10;

        [SerializeField] private Boost _boost;
        [SerializeField] private int _priceBuyBoost;
        [SerializeField] private int _priceUpgradeBoost;
        [SerializeField] private Button _buyForMoney;
        [SerializeField] private Button _buyForAd;
        [SerializeField] private Button _upgradeForMoney;
        [SerializeField] private Button _upgradeForAd;
        [SerializeField] private TMP_Text _workTimeText;
        [SerializeField] private TMP_Text _countBoosts;
        [SerializeField] private int _buyId;
        [SerializeField] private int _upgradeId;

        private float _workTime = 5;
        private int _countUpgrade;
        private TMP_Text _priceBuyText;
        private TMP_Text _priceUpgradeText;
        private ShopBoosts _shopBoosts;
        private bool _isBanUpgrade = false;

        private void OnEnable()
        {
            _buyForMoney.onClick.AddListener(Buy);
            _buyForAd.onClick.AddListener(OnBuyForAd);
            _upgradeForMoney.onClick.AddListener(BuyUpgrade);
            _upgradeForAd.onClick.AddListener(OnUpgradeForAd);
        }

        private void Awake()
        {
            _priceBuyText = _buyForMoney.GetComponentInChildren<TMP_Text>();
            _priceUpgradeText = _upgradeForMoney.GetComponentInChildren<TMP_Text>();
            _shopBoosts = GetComponentInParent<ShopBoosts>();

            UpdateText();
        }

        private void OnDisable()
        {
            _buyForMoney.onClick.RemoveListener(Buy);
            _buyForAd.onClick.RemoveListener(OnBuyForAd);
            _upgradeForMoney.onClick.RemoveListener(BuyUpgrade);
            _upgradeForAd.onClick.RemoveListener(OnUpgradeForAd);
        }

        public void SelectAdButtons()
        {
            int chanceBuy = Random.Range(0, 100);
            int chanceUpgrade = Random.Range(0, 100);

            if (_boost.Count < _minBoosts && chanceBuy <= _maxChance)
                _buyForAd.gameObject.SetActive(true);
            else if (chanceBuy <= _minChance)
                _buyForAd.gameObject.SetActive(true);
            else
                _buyForAd.gameObject.SetActive(false);

            if (chanceUpgrade <= _minChance)
                _upgradeForAd.gameObject.SetActive(true);
            else
                _upgradeForAd.gameObject.SetActive(false);
        }

        public void RewardBoost()
        {
            _shopBoosts.Buy(_boost, 0);
            BuyBoost();
        }

        public void RewardUpgradeBoost()
        {
            if (_isBanUpgrade == false)
            {
                _shopBoosts.BuyUpgrade(_boost, 0);
                UpgradeBoost();
            }
        }

        public void Load()
        {
            _boost.Load();
            _countUpgrade = _boost.CountUpgrade;
            _workTime = _boost.Time;

            if (_countUpgrade >= _maxUpgradeBoost)
                BanUpgrade();
        }

        private void BuyBoost()
        {
            _workTime = _boost.Time;
            UpdateText();
        }

        private void UpgradeBoost()
        {
            _workTime = _boost.Time;
            _countUpgrade++;
            UpdateText();

            if (_countUpgrade >= _maxUpgradeBoost)
                BanUpgrade();
        }

        private void Buy()
        {
            _shopBoosts.Buy(_boost, _priceBuyBoost);
            BuyBoost();
        }

        private void BuyUpgrade()
        {
            if (_isBanUpgrade == false)
            {
                _shopBoosts.BuyUpgrade(_boost, _priceUpgradeBoost);
                UpgradeBoost();
            }
        }

        private void OnBuyForAd()
        {
            YandexGame.RewVideoShow(_buyId);
        }

        private void OnUpgradeForAd()
        {
            YandexGame.RewVideoShow(_upgradeId);
        }

        private void UpdateText()
        {
            Load();

            _priceBuyText.text = _priceBuyBoost.ToString();
            _priceUpgradeText.text = _priceUpgradeBoost.ToString();
            _workTimeText.text = _workTime.ToString();
            _countBoosts.text = _boost.Count.ToString();
        }

        private void BanUpgrade()
        {
            _isBanUpgrade = true;
            _upgradeForAd.interactable = false;
            _upgradeForMoney.interactable = false;
        }
    }
}